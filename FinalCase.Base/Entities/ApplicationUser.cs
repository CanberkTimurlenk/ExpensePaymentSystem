namespace FinalCase.Base.Entities;
public abstract class ApplicationUser : BaseEntityWithId
{
    // All users will be derived from this class

    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
    public bool IsActive { get; set; }
}
