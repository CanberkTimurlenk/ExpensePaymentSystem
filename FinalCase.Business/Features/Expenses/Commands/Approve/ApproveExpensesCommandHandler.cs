﻿using AutoMapper;
using FinalCase.BackgroundJobs.Hangfire.Delayeds.Payment;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Payments.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Data.Enums;
using FinalCase.Schema.Email;
using FinalCase.Schema.Entity.Responses;
using FinalCase.Schema.ExternalApi;
using FinalCase.Services.NotificationService;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Expenses.Commands.Approve;

/// <summary>
/// Handles the approval of a set of pending employee expenses.
/// </summary>
public class ApproveExpensesCommandHandler(FinalCaseDbContext dbContext, INotificationService notificationService, IMapper mapper)
    : IRequestHandler<ApproveExpensesCommand, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    private readonly INotificationService notificationService = notificationService;

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(ApproveExpensesCommand request, CancellationToken cancellationToken)
    {
        var expenses = await GetExpenses(request, cancellationToken);
        expenses.ForEach(e => e.Status = ExpenseStatus.Approved); // Change the status of the expenses to Approved.

        var payments = mapper.Map<IEnumerable<Payment>>(expenses);


        await dbContext.Payments.AddRangeAsync(payments, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Payment creation to finalize approved expenses. 
        SendPayments(payments, cancellationToken);
        // The job sends a request to the banking system to check the status of the payment.
        // If the response is completed, the payment status in the db will be marked as completed        

        var response = mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(response);
    }
    private async Task<IEnumerable<Expense>> GetExpenses(ApproveExpensesCommand request, CancellationToken cancellationToken)
    {
        var expenseIds = request.ExpenseIdsToApprove.Select(e => e.Id).ToList(); // Get the expense ids from the request.

        return await dbContext.Expenses
            .Include(e => e.CreatorEmployee)
            .Include(e => e.Category)
            .Include(e => e.PaymentMethod)
            .Where(e => expenseIds.Contains(e.Id)) // If the current value of the e.Id exists in the expenseIds list, select the expense.
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Sends payment requests and corresponding emails for a collection of payments.
    /// </summary>
    /// <param name="payments">The collection of payments to be processed.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    private void SendPayments(IEnumerable<Payment> payments, CancellationToken cancellationToken)
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
            // Mapping profile is also exists, "mapper.Map<OutgoingPaymentRequest>(p)" can be used instead.
            new Email()
            {
                Subject = string.Format(PaymentEmailConstants.CompletedBody, p.ReceiverName, p.Amount, p.Expense.Date),
                Body = PaymentEmailConstants.CompletedSubject,
                To = new List<string>() { p.Employee.Email }
            },
            notificationService,
            cancellationToken);
        });
    }
}