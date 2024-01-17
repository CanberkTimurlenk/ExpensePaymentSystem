using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Update;

public record UpdatePaymentMethodCommand(int Id, PaymentMethodRequest Model) : IRequest<ApiResponse>;