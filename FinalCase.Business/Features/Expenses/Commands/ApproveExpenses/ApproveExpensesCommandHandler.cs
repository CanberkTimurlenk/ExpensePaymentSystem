using AutoMapper;
using FinalCase.BackgroundJobs.Hangfire.Delayeds.Payment;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Expenses.Commands.ApprovePendingExpenses;
using FinalCase.Business.Features.Payments.Constants.Email;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Data.Enums;
using FinalCase.Schema.Email;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;
using LinqKit;
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
        var expenses = await GetExpenses(request, cancellationToken);
        expenses.ForEach(e => e.Status = ExpenseStatus.Approved); // Change the status of the expenses to Approved.

        var payments = mapper.Map<IEnumerable<Payment>>(expenses);
        // The job sends a request to the banking system to check the status of the payment.
        // If the payment is completed, the payments status will be marked as completed, and the employee will be notified.
        // If the payment is not completed, the job will send the payment request to the banking system again.
        // If the payments is completed, the employee will be notified.        

        payments.ForEach(p => p.Method = ""); // TODO

        await dbContext.Payments.AddRangeAsync(payments, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Payment creation to finalize approved expenses. 
        SendPayments(payments, cancellationToken);

        var response = mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
    private async Task<IEnumerable<Expense>> GetExpenses(ApproveExpensesCommand request, CancellationToken cancellationToken)
    {
        var expenseIds = request.ExpenseIdsToApprove.Select(e => e.Id).ToList(); // Get the expense ids from the request.

        return await dbContext.Expenses
            .Include(e => e.CreatorEmployee)
            .Where(e => expenseIds.Contains(e.Id)) // If the current value of the e.Id exists in the expenseIds list, select the expense.
            .ToListAsync(cancellationToken);
    }
    private static void SendPayments(IEnumerable<Payment> payments, CancellationToken cancellationToken)
    {
        payments.ForEach(p =>
        {
            PaymentJobs.SendPaymentRequest(new OutgoingPaymentRequest
            {
                Amount = p.Amount,
                Description = p.Description,
                ReceiverIban = p.ReceiverIban,
                ReceiverName = p.ReceiverName,
            },
            // Instead of mapping a single object with AutoMapper in each iteration,
            // I prefer to map it manually to make it more efficient.
            // Mapping profile is also exists "mapper.Map<OutgoingPaymentRequest>(p)" 
            new Email
            {
                Body = string.Format(PaymentEmailConstants.CompletedBody, p.ReceiverName, p.Amount, p.Expense.Date),
                Subject = PaymentEmailConstants.CompletedSubject,
                To = p.Employee.Email
            }
            , cancellationToken);
        });
    }
}