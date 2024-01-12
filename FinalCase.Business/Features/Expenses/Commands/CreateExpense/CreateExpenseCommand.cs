using FinalCase.Base.Response;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.CreateExpense;
public record CreateExpenseCommand(int CreatorEmployeeId, ExpenseRequest Model) : IRequest<ApiResponse<ExpenseResponse>>;