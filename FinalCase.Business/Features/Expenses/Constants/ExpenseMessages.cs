namespace FinalCase.Business.Features.Expenses.Constants;
public static class ExpenseMessages
{
    public const string ExpenseNotFound = "Expense not found.";

    public const string CompletedUpdateError = "Cannot update a completed expense";
    public const string RejectedUpdateError = "Cannot update a rejected expense";
    public const string OnlyPendingUpdateError = "Only the 'pending' status is allowed to update an expense";

    public const string PaymentMethodNotFound = "The specified Payment Method was not found.";
    public const string CategoryNotFound = "The specified Category was not found.";


    public const string ExpenseAlreadyApprovedError = "Already approved expenses exist. An expense cannot be approved more than once.";


}
