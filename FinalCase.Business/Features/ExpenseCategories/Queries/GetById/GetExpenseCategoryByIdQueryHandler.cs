using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Business.Features.ExpenseCategories.Queries.GetById;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace FinalCase.Business.Features.ExpenseCategories.Queries.GetById;
public class GetExpenseCategoryByIdQueryHandler(FinalCaseDbContext dbContext, IDistributedCache cache, IMapper mapper) : IRequestHandler<GetExpenseCategoryByIdQuery, ApiResponse<ExpenseCategoryResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IDistributedCache cache = cache;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var byteArr = await cache.GetAsync(ExpenseCategoryCacheKeys.AllExpenseCategories, cancellationToken);

        ExpenseCategoryResponse? response;

        if (byteArr != null)
        {
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<IEnumerable<ExpenseCategoryResponse>>>
                (Encoding.UTF8.GetString(byteArr));

            response = apiResponse?.Response.FirstOrDefault(ec => ec.Id.Equals(request.Id));
            return new ApiResponse<ExpenseCategoryResponse>(response!);
        }

        var expenseCategory = await dbContext.ExpenseCategories
           .AsNoTracking()
           .FirstOrDefaultAsync(ec => ec.Id.Equals(request.Id), cancellationToken);

        response = mapper.Map<ExpenseCategoryResponse>(expenseCategory);
        return new ApiResponse<ExpenseCategoryResponse>(response);
    }
}
