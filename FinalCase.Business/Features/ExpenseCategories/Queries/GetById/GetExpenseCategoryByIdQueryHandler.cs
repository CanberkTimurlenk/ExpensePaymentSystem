using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Queries.GetById;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ExpenseCategories.Queries.GetById;
public class GetExpenseCategoryByIdQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetExpenseCategoryByIdQuery, ApiResponse<ExpenseCategoryResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var expenseCategories = await dbContext.ExpenseCategories
            .AsNoTracking() // disable tracking to improve performance                                    
            .FirstOrDefaultAsync(ec => ec.Id.Equals(request.Id), cancellationToken);

        var response = mapper.Map<ExpenseCategoryResponse>(expenseCategories);

        return new ApiResponse<ExpenseCategoryResponse>(response);
    }
}
