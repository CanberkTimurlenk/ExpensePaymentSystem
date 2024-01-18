using FinalCase.Base.Response;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Delete;
public record DeleteExpenseCategoryCommand(int Id) : IRequest<ApiResponse>;