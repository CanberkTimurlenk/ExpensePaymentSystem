using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace FinalCase.Business.Features.Pipelines.Cache;
public class CacheRemovingBehavior<TRequest, TResponse>(IDistributedCache cache) : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>, ICacheRemoverRequest
{
    private readonly IDistributedCache cache = cache;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
            return await next();

        TResponse response = await next();

        if (request.CacheKey != null)
            await cache.RemoveAsync(request.CacheKey, cancellationToken);

        return response;
    }
}
