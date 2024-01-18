using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema.Entity.Requests;

public class ApplicationUserRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    [JsonIgnore]
    public string Role { get; set; }
}
