namespace FinalCase.Business.Features.Expenses.Constants;
public static class ExpenseMessages
{
    public const string ExpenseNotFound = "Expense not found.";

    public const string CompletedUpdateError = "Cannot update a completed expense";
    public const string RejectedUpdateError = "Cannot update a rejected expense";
    public const string OnlyPendingUpdateError = "Only the 'pending' status is allowed to update an expense";

    public const string PaymentMethodNotFound = "The specified Payment Method was not found.";
    public const string CategoryNotFound = "The specified Category was not found.";

    public const string ExpenseAlreadyApprovedError = "Some expenses have already been approved or rejected. Only pending expenses can be requested. Related Ids: {0}";
    public const string ExpenseToApprovedNotFoundError = "Some expenses that were requested for approval were not found. Related Ids: {0}";


    public const string UnauthorizedExpenseUpdate = "You do not have permission to update this expense.";
    public const string UnauthorizedExpenseDelete = "You do not have permission to delete this expense.";
    public const string UnauthorizedExpenseRead = "You do not have permission to access this expense.";
}

