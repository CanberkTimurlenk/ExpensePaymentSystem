using FinalCase.Base.Response;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.Delete;

public record DeleteExpenseCommand(int UserId, string Role, int Id) : IRequest<ApiResponse>
{

}
