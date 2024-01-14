namespace FinalCase.Schema.Requests;

/// <summary>
/// The request model will be used as payment request to be sent to the banking system.
/// </summary>
public record OutgoingPaymentRequest
{
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public string ReceiverIban { get; init; }
    public string ReceiverName { get; init; }
    public string Description { get; init; }
}