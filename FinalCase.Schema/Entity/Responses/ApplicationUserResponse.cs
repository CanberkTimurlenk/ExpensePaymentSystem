using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema.Entity.Responses;

/// <summary>
/// Base response for all application user responses
/// </summary>
public class ApplicationUserResponse : BaseResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public DateTime LastActivityDate { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
}
