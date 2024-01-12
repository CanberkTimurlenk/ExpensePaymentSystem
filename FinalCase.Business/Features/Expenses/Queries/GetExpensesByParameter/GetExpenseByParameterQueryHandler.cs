using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Responses;
using MediatR;
using LinqKit;
using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static FinalCase.Base.Helpers.Linq.LinqHelper;

namespace FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;

public class GetExpenseByParameterQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    // primary constructor
    : IRequestHandler<GetExpensesByParameterQuery, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(GetExpensesByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Expense>(true);

        // updates the predicate with LinqKit, if the condition is true
        predicate = AddCondition(request.CreatorEmployeeId != default, predicate, e => e.CreatorEmployeeId == request.CreatorEmployeeId);
        predicate = AddCondition(request.CategoryId != default, predicate, e => e.CategoryId == request.CategoryId);
        predicate = AddCondition(request.MinAmount != default, predicate, e => e.Amount >= request.MinAmount);
        predicate = AddCondition(request.MaxAmount != default, predicate, e => e.Amount <= request.MaxAmount);
        predicate = AddCondition(request.InitialDate != default, predicate, e => e.Date >= request.InitialDate);
        predicate = AddCondition(request.FinalDate != default, predicate, e => e.Date <= request.FinalDate);
        predicate = AddCondition(!string.IsNullOrEmpty(request.Location), predicate, e => e.Location == request.Location);

        var expenses = await dbContext.Expenses
            .Where(predicate)
            .Include(e => e.Category)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var response = mapper.Map<List<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
}