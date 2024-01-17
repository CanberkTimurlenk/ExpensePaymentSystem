using System.Text.Json;

namespace FinalCase.Schema.ExternalApi;

/// <summary>
/// The request model will be used as payment request to be sent to the banking system.
/// </summary>
public record OutgoingPaymentRequest
{
    public decimal Amount { get; init; }
    public string ReceiverIban { get; init; }
    public string ReceiverName { get; init; }
    public string Description { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}