using FinalCase.Base.Response;
using MediatR;

namespace FinalCase.Business.Features.Payments.Commands.Delete;

public record DeletePaymentCommand(int id) : IRequest<ApiResponse>;