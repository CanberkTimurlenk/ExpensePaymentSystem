namespace FinalCase.Schema.Responses;

public record AuthenticationResponse
{
    public DateTime ExpireDate { get; init; }
    public string Token { get; init; }
    public string Email { get; init; }
}
