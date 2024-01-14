using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Expenses.Commands.ApprovePendingExpenses;
using FinalCase.Data.Contexts;
using FinalCase.Data.Enums;
using FinalCase.Schema.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Expenses.Commands.ApproveExpenses;

/// <summary>
/// Handles the approval of a set of pending employee expenses.
/// </summary>
public class ApproveExpensesCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<ApproveExpensesCommand, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(ApproveExpensesCommand request, CancellationToken cancellationToken)
    {
        var expenseIds = request.Model.Select(e => e.Id).ToList(); // Get the expense ids from the request.

        var expenses = await dbContext.Expenses
            .Where(e => expenseIds.Contains(e.Id)) // If the current value of the e.Id exists in the expenseIds list, select the expense.
            .ToListAsync(cancellationToken);

        expenses.ForEach(e => e.Status = ExpenseStatus.Approved); // Change the status of the expenses to Approved.

        await dbContext.SaveChangesAsync(cancellationToken);

        

        var response = mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
}