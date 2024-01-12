namespace FinalCase.Schema.Requests;
public record AuthenticationRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
}
