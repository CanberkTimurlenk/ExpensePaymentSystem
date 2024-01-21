using FinalCase.Business.Features.Authentication.Constants.Jwt;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalCase.Api.Filters
{
    /// <summary>
    /// Verifies the identity by comparing the 'id' from the query of an endpoint with the one from the token for Role: Employee.
    /// - If they match, the controller method will be executed.
    /// - If they differ, the result will be Forbidden(403).
    /// - If the token does not contain an 'id', the result will be Unauthorized(401).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EmployeeIdFromQueryAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRoleClaim = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRoleClaim != Roles.Employee)    
                return;

            var userIdClaim = context.HttpContext.User.FindFirst(JwtPayloadFields.Id)?.Value;

            string employeeIdFromQuery = context.HttpContext.Request.Query["employeeId"];

            if (userIdClaim != employeeIdFromQuery)
                context.Result = new ForbidResult();
        }
    }
}
