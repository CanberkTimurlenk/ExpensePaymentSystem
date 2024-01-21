using AutoMapper;
using FinalCase.BackgroundJobs.Hangfire.Delayeds.Payment;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Expenses.Constants;
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

        List<string> errors =
            [
                CheckIfNotFound(request, expenses), // checks if the expense not found
                CheckIfAlreadyApprovedOrRejected(expenses), // check if the expense is already done (approved or rejected)
            ];

        var errorMessage = string.Join("-", errors.Where(s => !string.IsNullOrEmpty(s)));
        // if the error message is not null or empty then join it with '-'
        // in the front end, the message could be splitted with '-' character and used.

        if (!string.IsNullOrEmpty(errorMessage))
            return new ApiResponse<IEnumerable<ExpenseResponse>>(errorMessage); // returns the error message if exists

        expenses.ForEach(e => e.Status = ExpenseStatus.Approved); // Change the status of the expenses to Approved.

        foreach (var item in expenses)
        {
            item.Status = ExpenseStatus.Approved;
            item.ReviewerAdminId = request.ReviewerAdminId;
        }

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
            .Include(e => e.Documents)
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
        foreach (var payment in payments)
        {
            var outgoingPaymentRequest = new OutgoingPaymentRequest
            {
                Amount = payment.Amount,
                Description = payment.Id.ToString(),
                ReceiverIban = payment.ReceiverIban,
                ReceiverName = payment.ReceiverName
            };// Instead of mapping a single object with AutoMapper in each iteration,
              // I prefer to map it manually to make it more efficient.
              // Mapping profile is also exists, "mapper.Map<OutgoingPaymentRequest>(p)" can be used instead.

            var email = new Email
            {
                Subject = string.Format(PaymentEmailConstants.CompletedBody, payment.ReceiverName, payment.Amount, payment.Expense.Date),
                Body = PaymentEmailConstants.CompletedSubject,
                To = new List<string> { payment.Employee.Email }
            };

            PaymentJobs.SendPaymentRequest(outgoingPaymentRequest, email, notificationService, cancellationToken);
        }
    }

    /// <summary>
    /// Checks if the expenses are not found in the db.
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="expenses">The expenses to check (retrieved from the db)</param>
    /// <returns>A set of ids with ExpenseToApprovedNotFoundError Message</returns>
    public static string CheckIfNotFound(ApproveExpensesCommand request, IEnumerable<Expense> expenses)
    {
        var expenseIdsInRequest = request.ExpenseIdsToApprove.Select(r => r.Id); // ids from the request.
        var expenseIdsInDb = expenses.Select(e => e.Id); // ids from the db.

        var notFoundIds = expenseIdsInRequest.Except(expenseIdsInDb); // the ids that are not found in the db, but exist in the request.

        if (notFoundIds.Any())
        {
            var ids = string.Join(", ", notFoundIds);
            return string.Format(ExpenseMessages.ExpenseToApprovedNotFoundError, ids);
        }
        return null;
    }

    /// <summary>
    /// Checks if the expenses are already approved.
    /// </summary>
    /// <param name="expenses">The expenses to check (retrieved from the db)</param>
    /// <returns>A set of ids with ExpenseAlreadyApprovedError Message</returns>
    public static string CheckIfAlreadyApprovedOrRejected(IEnumerable<Expense> expenses)
    {
        var alreadyApproveds = expenses.Where(e => e.Status == ExpenseStatus.Approved || e.Status == ExpenseStatus.Rejected);

        if (alreadyApproveds.Any()) // approved expense can not be approved again.
        {
            var idList = alreadyApproveds.Select(e => e.Id);
            var ids = string.Join(", ", idList);

            return string.Format(ExpenseMessages.ExpenseAlreadyApprovedError, ids);
        }
        return null;
    }
}
