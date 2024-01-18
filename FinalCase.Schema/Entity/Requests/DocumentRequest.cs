using System.Text.Json.Serialization;

namespace FinalCase.Schema.Entity.Requests;

public class DocumentRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public int ExpenseId { get; set; }
}
