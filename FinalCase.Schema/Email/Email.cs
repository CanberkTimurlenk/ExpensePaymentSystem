using System.Text.Json;

namespace FinalCase.Schema.Email;

public record Email
{
    public string Subject { get; init; }
    public string Body { get; init; }
    public string To { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public Email(string subject, string body, string to)
    {
        Subject = subject;
        Body = body;
        To = to;
    }
}
