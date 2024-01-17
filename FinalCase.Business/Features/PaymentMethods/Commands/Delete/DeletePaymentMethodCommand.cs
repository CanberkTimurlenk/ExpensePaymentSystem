using FinalCase.Base.Response;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Delete;

public record DeletePaymentMethodCommand(int Id) : IRequest<ApiResponse>;