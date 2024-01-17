using System.Text.Json.Serialization;

namespace FinalCase.Schema.Entity.Requests;

public class PaymentMethodRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
