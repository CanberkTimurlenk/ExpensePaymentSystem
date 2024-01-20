namespace FinalCase.Business.Features.Pipelines.Cache;

public interface ICachableRequest
{
    string CacheKey { get; }
    bool BypassCache { get; }
    TimeSpan? SlidingExpiration { get; }
}