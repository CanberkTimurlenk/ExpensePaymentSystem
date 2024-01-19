using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Expenses.Queries.GetAllExpenses;
public class GetAllExpensesQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllExpensesQuery, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
    {
        var x = await dbContext.Expenses.Include(e => e.CreatorEmployee).IgnoreQueryFilters().ToListAsync(cancellationToken);

        var expenses = await dbContext.Expenses
                .Include(e => e.Category)
                .Include(e => e.CreatorEmployee)
                .Include(e => e.PaymentMethod)
                .Include(e => e.ReviewerAdmin)
                .Include(e => e.Payment)
                .Include(e => e.Documents)
                .ProjectTo<ExpenseResponse>(mapper.ConfigurationProvider)
                .AsNoTrackingWithIdentityResolution() // Since the operation is read-only, this method can be used to improve performance
                .ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
}
