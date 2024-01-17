namespace FinalCase.Api.Constants.Controller;

public static class ControllerConstants
{
    /// <summary>
    /// Constant representing the parameter name for the employee ID (which is the Identity of the ApplicationUser).
    /// The existence of this constant is primarily to maintain consistency and prevent the use of magic strings.
    /// It is referenced in another class, <see cref="Filters.AuthorizeIdMatchAttribute"/>.
    /// </summary>
    public const string EmployeeId = "employee-id"; // the id of ApplicationUser
    
}
