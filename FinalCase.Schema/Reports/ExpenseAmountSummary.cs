namespace FinalCase.Schema.Reports;
/// <summary>
/// Schema for
///<para> - Daily Expense Amount Summary </para>
///<para> - Weekly Expense Amount Summary </para>
///<para> - Monthly Expense Amount Summary </para>
/// </summary>
public record ExpenseAmountSummary
{
    public DateTime StartDateTime { get; init; }
    public DateTime FinalDateTime { get; init; }
    public decimal PendingAmount { get; init; }
    public decimal ApprovedAmount { get; init; }
    public decimal RejectedAmount { get; init; }
}
