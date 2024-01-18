namespace FinalCase.Business.Features.Expenses.Constants;
public static class ExpenseMessages
{
    public const string ExpenseNotFound = "Expense not found.";
    public const string CompletedUpdateError = "Cannot update a completed expense";
    public const string RejectedUpdateError = "Cannot update a rejected expense";
    public const string OnlyPendingUpdateError = "Only the 'pending' status is allowed to update an expense";

}
