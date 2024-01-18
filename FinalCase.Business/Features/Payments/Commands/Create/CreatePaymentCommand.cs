using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Payments.Commands.Create;
public record CreatePaymentCommand(PaymentRequest Model) : IRequest<ApiResponse<PaymentResponse>>;