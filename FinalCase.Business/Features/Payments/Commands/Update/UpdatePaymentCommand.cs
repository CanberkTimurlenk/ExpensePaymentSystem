using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Payments.Commands.Update;
public record UpdatePaymentCommand(int EmployeeId, int ExpenseId, PaymentRequest Model) : IRequest<ApiResponse>;