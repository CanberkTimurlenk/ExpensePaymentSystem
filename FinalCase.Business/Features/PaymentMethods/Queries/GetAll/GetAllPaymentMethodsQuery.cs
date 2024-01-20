using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Business.Features.PaymentMethods.Constants;
using FinalCase.Business.Features.Pipelines.Cache;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Queries.GetAll;
public record GetAllPaymentMethodsQuery : IRequest<ApiResponse<IEnumerable<PaymentMethodResponse>>>,
    ICachableRequest
{
    public string CacheKey => PaymentMethodCacheKeys.AllPaymentMethods;
    public bool BypassCache { get; }
    public TimeSpan? SlidingExpiration { get; }
}