using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using MediatR;
using LinqKit;
using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        var predicate = PredicateBuilder.New<Expense>(true);

        if (p.EmployeeId != default)
            predicate = predicate.And(e => e.CreatorEmployeeId == p.EmployeeId);
        
        if (p.CategoryId != default)
            predicate = predicate.And(e => e.CategoryId == p.CategoryId);
        
        if (p.PaymentMethodId != default)
            predicate = predicate.And(e => e.PaymentMethodId == p.PaymentMethodId);
        
        if (p.MinAmount != default)
            predicate = predicate.And(e => e.Amount >= p.MinAmount);
        
        if (p.MaxAmount != default)
            predicate = predicate.And(e => e.Amount <= p.MaxAmount);
        
        if (p.InitialDate != default)
            predicate = predicate.And(e => e.Date >= p.InitialDate);
        
        if (p.FinalDate != default)
            predicate = predicate.And(e => e.Date <= p.FinalDate);
        
        if (p.Status != default)
            predicate = predicate.And(e => e.Status == p.Status);
        
        if (!string.IsNullOrEmpty(p.Location))
            predicate = predicate.And(e => e.Location == p.Location);

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