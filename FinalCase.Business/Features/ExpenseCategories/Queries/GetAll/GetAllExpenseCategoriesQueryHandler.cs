using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ExpenseCategories.Queries.GetAll;
public class GetAllExpenseCategoriesQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllExpenseCategoriesQuery, ApiResponse<IEnumerable<ExpenseCategoryResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<ExpenseCategoryResponse>>> Handle(GetAllExpenseCategoriesQuery request, CancellationToken cancellationToken)
    {
        var expenseCategories = await dbContext.ExpenseCategories
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<ExpenseCategoryResponse>>(expenseCategories);

        return new ApiResponse<IEnumerable<ExpenseCategoryResponse>>(response);
    }
}
