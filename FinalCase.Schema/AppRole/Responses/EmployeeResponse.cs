using FinalCase.Schema.Entity.Responses;

namespace FinalCase.Schema.AppRoles.Responses;

public class EmployeeResponse : ApplicationUserResponse
{
    public string Iban { get; set; }
    public virtual ICollection<ExpenseResponse>? CreatedExpenses { get; set; }
}
