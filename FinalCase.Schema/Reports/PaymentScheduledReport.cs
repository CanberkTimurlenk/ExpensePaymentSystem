namespace FinalCase.Schema.Reports;
/// <summary>
/// Schema for
///<para> - Daily Payment Report </para>
///<para> - Weekly Payment Report</para>
///<para> - Monthly Payment Report</para>
/// </summary>
public record PaymentScheduledReport
{
    public decimal Amount { get; init; }
    public string Description { get; init; }
    public string ReceiverName { get; init; }
    public string PaymentMethodName { get; init; }
    public string ReceiverIban { get; init; }
    public DateTime Date { get; init; }
}
