using FinalCase.Base.Schema;

namespace FinalCase.Schema.Entity.Responses;
public class ApplicationUserResponse : BaseResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string? Iban { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<ExpenseResponse>? CreatedExpenses { get; set; }
}
