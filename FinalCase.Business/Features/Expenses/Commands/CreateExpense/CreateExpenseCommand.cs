using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.CreateExpense;
public record CreateExpenseCommand(int CreatorEmployeeId, ExpenseRequest Model) : IRequest<ApiResponse<ExpenseResponse>>;