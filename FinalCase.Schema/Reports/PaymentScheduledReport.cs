namespace FinalCase.Schema.Reports;
public record PaymentScheduledReport
{
    public decimal Amount { get; init; }
    public string Description { get; init; }
    public string ReceiverName { get; init; }
    public string PaymentMethodName { get; init; }
    public string ReceiverIban { get; init; }
    public DateTime Date { get; init; }
}
