using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Queries.GetById;
public record GetPaymentMethodByIdQuery(int Id) : IRequest<ApiResponse<PaymentMethodResponse>>;