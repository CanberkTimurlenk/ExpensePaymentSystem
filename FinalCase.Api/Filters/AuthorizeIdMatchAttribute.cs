using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FinalCase.Api.Constants.Controller;

namespace FinalCase.Api.Filters;

/// <summary>
/// Verifies the identity by comparing the 'id' from the route of an endpoint with the one from the token.
/// - If they match, the controller method will be executed.
/// - If they differ, the result will be Forbidden(403).
/// - If the token does not contain an 'id', the result will be Unauthorized(401).
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeIdMatchAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst("Id")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            context.Result = new UnauthorizedResult();

        var idFromRoute = context.RouteData.Values[ControllerConstants.EmployeeId] as string;

        if (userIdClaim != idFromRoute)
            context.Result = new ForbidResult();
    }
}
