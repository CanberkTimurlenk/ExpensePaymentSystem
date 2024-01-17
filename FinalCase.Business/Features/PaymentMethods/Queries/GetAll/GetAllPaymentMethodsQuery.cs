using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Queries.GetAll;
public record GetAllPaymentMethodsQuery : IRequest<ApiResponse<IEnumerable<PaymentMethodResponse>>>;