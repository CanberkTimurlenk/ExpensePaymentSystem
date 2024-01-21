using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;
public class ApplicationUser : BaseEntityWithId
{
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime LastActivityDate { get; set; }
    public string? Iban { get; set; }
    public string Role { get; set; }
    public virtual ICollection<Expense>? CreatedExpenses { get; set; }
    public virtual ICollection<Payment>? Payments { get; set; } // ExpenseStatus = Approved and bank transfer is done
}