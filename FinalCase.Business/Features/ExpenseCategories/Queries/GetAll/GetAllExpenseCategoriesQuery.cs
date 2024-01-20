using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Business.Features.Pipelines.Cache;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Queries.GetAll;
public record GetAllExpenseCategoriesQuery : IRequest<ApiResponse<IEnumerable<ExpenseCategoryResponse>>>,
    ICachableRequest
{
    public string CacheKey => ExpenseCategoryCacheKeys.AllExpenseCategories;

    public bool BypassCache { get; }

    public TimeSpan? SlidingExpiration { get; }
}