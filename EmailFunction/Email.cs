public record Email
{
    public string Subject { get; init; }
    public string Body { get; init; }
    public string From { get; init; }
    public string To { get; init; }
}
