namespace FinalCase.Schema.Email
{
    public record Email
    {
        public string Subject { get; init; }
        public string Body { get; init; }
        public string To { get; init; }
    }
}
