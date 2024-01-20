using FinalCase.Base.Response;
using FinalCase.Business.Features.PaymentMethods.Constants;
using FinalCase.Business.Features.Pipelines.Cache;
using FinalCase.Schema.Entity.Requests;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Update;
public record UpdatePaymentMethodCommand(int Id, PaymentMethodRequest Model) : IRequest<ApiResponse>,
    ICacheRemoverRequest
{
    public string? CacheKey => PaymentMethodCacheKeys.AllPaymentMethods;
    public bool BypassCache { get; }
}