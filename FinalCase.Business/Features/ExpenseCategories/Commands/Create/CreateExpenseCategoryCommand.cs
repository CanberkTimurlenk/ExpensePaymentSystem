using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Create;
public record CreateExpenseCategoryCommand(ExpenseCategoryRequest Model) : IRequest<ApiResponse<ExpenseCategoryResponse>>;