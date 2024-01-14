using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Expenses.Commands.ApprovePendingExpenses;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Data.Enums;
using FinalCase.Schema.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;

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
            .Include(e => e.CreatorEmployee)
            .ThenInclude(e => e.Account)
            .Where(e => expenseIds.Contains(e.Id)) // If the current value of the e.Id exists in the expenseIds list, select the expense.
            .ToListAsync(cancellationToken);

        expenses.ForEach(e => e.Status = ExpenseStatus.Approved); // Change the status of the expenses to Approved.


        // Payment creation to finalize approved expenses. 

        var payments = mapper.Map<IEnumerable<Payment>>(expenses);
        // In case a payment results in failure due to resiliency issues,
        // a background service (Hangfire) will periodically poll the payments which is not completed.
        // Then the job sends a request to the banking system to check the status of the payment. (due to the resiliency reasons)
        // If the payment is completed, the payments status will be marked as completed, and the employee will be notified.
        // If the payment is not completed, the job will send the payment request to the banking system again.
        // If the payments is completed, the employee will be notified.
        // Resiliency issues are assumed to be the source of errors.

        var outgoingPayment = mapper.Map<IEnumerable<Payment>>(payments);

        // TODO 
        // Send the outgoing payments to the bank.

        await dbContext.Payments.AddRangeAsync(payments, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
}