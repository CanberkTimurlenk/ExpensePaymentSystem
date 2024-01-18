using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Payments.Commands.Create;
public record CreatePaymentCommand : IRequest<PaymentResponse>