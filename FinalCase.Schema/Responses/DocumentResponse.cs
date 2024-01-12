namespace FinalCase.Schema.Response;
public record DocumentResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Url { get; init; }
    public int ExpenseId { get; init; }
}
