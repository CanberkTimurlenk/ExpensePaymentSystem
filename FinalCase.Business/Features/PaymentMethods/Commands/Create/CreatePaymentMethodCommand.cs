using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Create;

public record CreatePaymentMethodCommand(PaymentMethodRequest Model) : IRequest<ApiResponse<PaymentMethodResponse>>;