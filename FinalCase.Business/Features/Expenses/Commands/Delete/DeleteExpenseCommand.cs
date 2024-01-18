using FinalCase.Base.Response;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.Delete;

public record DeleteExpenseCommand(int Id) : IRequest<ApiResponse>
{

}
