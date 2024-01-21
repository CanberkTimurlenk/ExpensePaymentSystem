using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Business.Features.Pipelines.Cache;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Create;
public record CreateExpenseCategoryCommand(int InsertUserId, ExpenseCategoryRequest Model) : IRequest<ApiResponse<ExpenseCategoryResponse>>,
    ICacheRemoverRequest
{
    public string? CacheKey => ExpenseCategoryCacheKeys.AllExpenseCategories;

    public bool BypassCache { get; }
}
