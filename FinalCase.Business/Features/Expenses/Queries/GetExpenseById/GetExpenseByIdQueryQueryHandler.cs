using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Expenses.Queries.GetAllExpenses;
public class GetExpenseByIdQueryQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        var expenses = await dbContext.Expenses
                                .Include(e => e.Category)
                                .Include(e => e.CreatorEmployee)
                                .Include(e => e.PaymentMethod)
                                .Include(e => e.ReviewerAdmin)
                                .Include(e => e.Payment)
                                .Include(e => e.Documents)
                                .AsNoTracking() // Since the operation is read-only, this method can be used to improve performance
                                .FirstOrDefaultAsync(e => e.Id.Equals(request.Id), cancellationToken);

        var response = mapper.Map<ExpenseResponse>(expenses);
        return new ApiResponse<ExpenseResponse>(response);
    }
}
