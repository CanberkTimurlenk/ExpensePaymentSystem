using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Data.Enums;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Expenses.Commands.RejectExpenses;

public class RejectExpensesCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<RejectExpensesCommand, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(RejectExpensesCommand request, CancellationToken cancellationToken)
    {
        var expenseIds = request.Model.Select(e => e.Id).ToList(); // Get the expense ids from the request.

        var expenses = await dbContext.Expenses
            .Where(e => expenseIds.Contains(e.Id)) // If the current value of the e.Id exists in the expenseIds list, select the expense.
            .ToListAsync(cancellationToken);

        var modelDictionary = request.Model.ToDictionary(r => r.Id, r => r.AdminDescription);
        // A dictionary is created to efficiently access the admin description(value) from the expense id(key).        

        expenses.ForEach(e =>
        {
            e.Status = ExpenseStatus.Rejected;
            e.AdminDescription = modelDictionary[e.Id];
        });

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
}