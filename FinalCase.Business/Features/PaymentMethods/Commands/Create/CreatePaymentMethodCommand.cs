using FinalCase.Base.Response;
using FinalCase.Business.Features.PaymentMethods.Constants;
using FinalCase.Business.Features.Pipelines.Cache;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Create;

public record CreatePaymentMethodCommand(PaymentMethodRequest Model) : IRequest<ApiResponse<PaymentMethodResponse>>,
    ICacheRemoverRequest
{
    public string? CacheKey => PaymentMethodCacheKeys.AllPaymentMethods;

    public bool BypassCache { get; }
}