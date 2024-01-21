using System.Text.Json;

public record Email
{
    public string Subject { get; init; }
    public string Body { get; init; }
    public string From { get; init; }
    public List<string> To { get; init; }


    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
