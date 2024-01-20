namespace FinalCase.Business.Features.Pipelines.Cache;

public interface ICacheRemoverRequest
{
    string? CacheKey { get; }
    bool BypassCache { get; }
}
