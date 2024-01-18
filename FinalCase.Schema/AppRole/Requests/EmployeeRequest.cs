using FinalCase.Schema.Entity.Requests;

namespace FinalCase.Schema.AppRoles.Requests;

public class EmployeeRequest : ApplicationUserRequest
{
    public string Iban { get; set; }
}
