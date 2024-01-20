using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Business.Features.Pipelines.Cache;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Delete;
public record DeleteExpenseCategoryCommand(int Id) : IRequest<ApiResponse>,
    ICacheRemoverRequest
{
    public string? CacheKey => ExpenseCategoryCacheKeys.AllExpenseCategories;

    public bool BypassCache { get; }
}
