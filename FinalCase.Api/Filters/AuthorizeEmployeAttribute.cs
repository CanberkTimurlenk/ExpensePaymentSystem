using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Filters;


/// <summary>
/// Verifies the identity by comparing the 'id' from the route with the one from the token.
/// - If they match, the controller method will be executed.
/// - If they differ, the request will be forbidden.
/// - If the token does not contain an 'id', the request will be an unauthorized.
/// </summary>

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeIdMatchAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst("Id")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var idFromRoute = context.RouteData.Values["id"] as string;

        if (!int.TryParse(idFromRoute, out var id) || !userIdClaim.Equals(id.ToString()))
            context.Result = new ForbidResult();
    }
}
