namespace FinalCase.Schema.Reports;

public record ExpenseAmountSummary
{
    public DateTime StartDateTime { get; init; }
    public DateTime FinalDateTime { get; init; }
    public decimal PendingAmount { get; init; }
    public decimal ApprovedAmount { get; init; }
    public decimal RejectedAmount { get; init; }
}
