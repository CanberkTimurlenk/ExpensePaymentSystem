using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Payments.Queries.GetById;

public record GetPaymentByIdQuery(int Id) : IRequest<ApiResponse<PaymentResponse>>;