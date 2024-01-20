using FinalCase.Base.Response;
using FinalCase.Business.Features.PaymentMethods.Constants;
using FinalCase.Business.Features.Pipelines.Cache;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Delete;

public record DeletePaymentMethodCommand(int Id) : IRequest<ApiResponse>,
    ICacheRemoverRequest
{
    public string? CacheKey => PaymentMethodCacheKeys.AllPaymentMethods;
    public bool BypassCache { get; }
}