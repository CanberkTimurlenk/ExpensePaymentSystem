using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using MediatR;
using LinqKit;
using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static FinalCase.Base.Helpers.Linq.ExpressionStarterExtensions;
using FinalCase.Schema.Entity.Responses;

namespace FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
public class GetExpenseByParameterQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    // primary constructor
    : IRequestHandler<GetExpensesByParameterQuery, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(GetExpensesByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Expense>(true)
            // An extension method was implemented to add a condition to the predicate based on the given boolean condition.                        
            .AddIf(request.CreatorEmployeeId != default, e => e.CreatorEmployeeId == request.CreatorEmployeeId)
            .AddIf(request.CategoryId != default, e => e.CategoryId == request.CategoryId)
            .AddIf(request.MinAmount != default, e => e.Amount >= request.MinAmount)
            .AddIf(request.MaxAmount != default, e => e.Amount <= request.MaxAmount)
            .AddIf(request.InitialDate != default, e => e.Date >= request.InitialDate)
            .AddIf(request.FinalDate != default, e => e.Date <= request.FinalDate)
            .AddIf(!string.IsNullOrEmpty(request.Location), e => e.Location == request.Location);

        var expenses = await dbContext.Expenses
            .Where(predicate)
            .Include(e => e.Category)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var response = mapper.Map<List<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
}