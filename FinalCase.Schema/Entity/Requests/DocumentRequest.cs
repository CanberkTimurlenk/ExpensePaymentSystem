using System.Text.Json.Serialization;

namespace FinalCase.Schema.Entity.Requests;

public record DocumentRequest
{
    [JsonIgnore]
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Url { get; init; }
    public int ExpenseId { get; init; }
}
