using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Business.Features.Pipelines.Cache;
using FinalCase.Schema.Entity.Requests;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Update;
public record UpdateExpenseCategoryCommand(int Id, ExpenseCategoryRequest Request) : IRequest<ApiResponse>,
     ICacheRemoverRequest
{
    public string? CacheKey => ExpenseCategoryCacheKeys.AllExpenseCategories;
    public bool BypassCache { get; }
}
