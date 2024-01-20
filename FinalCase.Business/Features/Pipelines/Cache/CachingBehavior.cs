using Azure;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace FinalCase.Business.Features.Pipelines.Cache;

public class CachingBehavior<TRequest, TResponse>(IDistributedCache cache, IConfiguration configuration)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly CacheSettings cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>()
        ?? throw new InvalidOperationException();

    private readonly IDistributedCache cache = cache;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache) // if bypass cache is true, execute the request and return the response
            return await next();

        byte[]? cachedResponse = await cache.GetAsync(request.CacheKey, cancellationToken);

        return cachedResponse != null
            ? JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse)) // if there is a cached response, return it
            ?? throw new InvalidOperationException()
            : await GetResponseAndAddToCache(request, next, cancellationToken); // if there is no cached response,
                                                                                // execute the request and add the response to the cache
    }

    private async Task<TResponse> GetResponseAndAddToCache(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse response = await next();

        TimeSpan slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(cacheSettings.SlidingExpiration);
        DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration };

        byte[] serializedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));

        await cache.SetAsync(request.CacheKey, serializedData, cacheOptions, cancellationToken);
        return response;
    }
}