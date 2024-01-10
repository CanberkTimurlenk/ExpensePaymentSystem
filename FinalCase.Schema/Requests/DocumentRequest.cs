namespace FinalCase.Schema.Requests;

public record DocumentRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Url { get; init; }
    public int ExpenseId { get; init; }
}
