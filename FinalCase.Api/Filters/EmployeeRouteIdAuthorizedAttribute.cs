using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FinalCase.Business.Features.Authentication.Constants.Jwt;
using System.Security.Claims;
using FinalCase.Business.Features.Authentication.Constants.Roles;

namespace FinalCase.Api.Filters;

/// <summary>
/// Verifies the identity by comparing the 'id' from the route of an endpoint with the one from the token for Role: Employee.
/// - If they match, the controller method will be executed.
/// - If they differ, the result will be Forbidden(403).
/// - If the token does not contain an 'id', the result will be Unauthorized(401).
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class EmployeeRouteIdAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(JwtPayloadFields.Id)?.Value;
        var userRoleClaim = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userRoleClaim))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var idFromRoute = context.RouteData.Values["employee-id"] as string;

        if (userIdClaim != idFromRoute && userRoleClaim.Equals(Roles.Employee))
            context.Result = new ForbidResult();
    }
}
