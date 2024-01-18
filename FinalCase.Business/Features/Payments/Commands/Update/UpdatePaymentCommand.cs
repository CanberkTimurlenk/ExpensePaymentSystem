using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Payments.Commands.Update;
public record UpdatePaymentCommand(int Id, PaymentRequest Model) : IRequest<ApiResponse>;