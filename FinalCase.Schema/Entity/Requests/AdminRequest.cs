using System.Text.Json.Serialization;

namespace FinalCase.Schema.Entity.Requests;

public record AdminRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Username { get; init; }
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public DateTime DateOfBirth { get; init; }
}
