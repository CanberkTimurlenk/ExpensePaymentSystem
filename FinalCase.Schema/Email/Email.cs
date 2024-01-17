using System.Text.Json;

namespace FinalCase.Schema.Email;

public record Email
{
    public string Subject { get; init; }
    public string Body { get; init; }
    public IEnumerable<string> To { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
 
}
