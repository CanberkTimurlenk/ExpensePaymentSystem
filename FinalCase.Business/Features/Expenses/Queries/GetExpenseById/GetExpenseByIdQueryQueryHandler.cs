using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.Expenses.Constants;
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
        var expense = await dbContext.Expenses
                                .Include(e => e.Category)
                                .Include(e => e.CreatorEmployee)
                                .Include(e => e.PaymentMethod)
                                .Include(e => e.ReviewerAdmin)
                                .Include(e => e.Payment)
                                .Include(e => e.Documents)
                                .Where(e => e.Id.Equals(request.Id))
                                .ProjectTo<ExpenseResponse>(mapper.ConfigurationProvider)
                                .AsNoTracking() // Since the operation is read-only, this method can be used to improve performance
                                                // project to behaves like select however, in select operation tracking is
                                                // disabled by default and not allowed to use. ProjectTo is allowed to use
                                .FirstOrDefaultAsync(cancellationToken);

        if (request.Role.Equals(Roles.Employee)
            && (expense == null
                || expense.CreatorEmployeeId != request.UserId))
        {
            return new ApiResponse<ExpenseResponse>(ExpenseMessages.UnauthorizedExpenseRead);
        }

        if (expense == null)
            return new ApiResponse<ExpenseResponse>(ExpenseMessages.ExpenseNotFound);


        var response = mapper.Map<ExpenseResponse>(expense);
        return new ApiResponse<ExpenseResponse>(response);
    }
}
