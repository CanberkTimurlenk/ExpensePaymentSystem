using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.Update;
public record UpdateExpenseCommand(int Id, ExpenseRequest Model) : IRequest<ApiResponse>;