using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using MediatR;
using LinqKit;
using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static FinalCase.Base.Helpers.Linq.ExpressionStarterExtensions;
using FinalCase.Schema.Entity.Responses;
using AutoMapper.QueryableExtensions;

namespace FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
public class GetExpenseByParameterQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    // primary constructor
    : IRequestHandler<GetExpensesByParameterQuery, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(GetExpensesByParameterQuery request, CancellationToken cancellationToken)
    {
        var p = request.Parameters;

        var predicate = PredicateBuilder.New<Expense>(true)
            // An extension method was implemented to add a condition to the predicate based on the given boolean condition.                        
            .AddIf(request.CreatorEmployeeId != default, e => e.CreatorEmployeeId == request.CreatorEmployeeId)
            .AddIf(p.CategoryId != default, e => e.CategoryId == p.CategoryId)
            .AddIf(p.PaymentMethodId != default, e => e.PaymentMethodId == p.PaymentMethodId)
            .AddIf(p.MinAmount != default, e => e.Amount >= p.MinAmount)
            .AddIf(p.MaxAmount != default, e => e.Amount <= p.MaxAmount)
            .AddIf(p.InitialDate != default, e => e.Date >= p.InitialDate)
            .AddIf(p.FinalDate != default, e => e.Date <= p.FinalDate)
            .AddIf(p.Status != default, e => e.Status == p.Status)
            .AddIf(!string.IsNullOrEmpty(p.Location), e => e.Location == p.Location);

        var expenses = await dbContext.Expenses
            .Where(predicate)
            .Include(e => e.Category)
            .ProjectTo<ExpenseResponse>(mapper.ConfigurationProvider)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
}