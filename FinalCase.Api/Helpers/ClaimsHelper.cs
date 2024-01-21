using FinalCase.Business.Features.Authentication.Constants.Jwt;
using System.Security.Claims;

namespace FinalCase.Api.Helpers;
public static class ClaimsHelper
{
    /// <summary>
    /// Get the user id and role from the claims if they exist
    /// </summary>
    /// <param name="identity">The claim identity</param>
    /// <param name="idClaimType">ID claim </param>
    /// <param name="roleClaimType">Role claim </param>
    /// <returns>the user id and role, tuple</returns>
    public static (int UserId, string Role) GetUserIdAndRoleFromClaims(ClaimsIdentity identity,
        string idClaimType = JwtPayloadFields.Id, string roleClaimType = ClaimTypes.Role)
    {
        var idClaim = identity.FindFirst(idClaimType);
        var roleClaim = identity.FindFirst(roleClaimType);

        if (idClaim == null || roleClaim == null)
            throw new ArgumentException("Invalid Claims");

        return (int.Parse(idClaim.Value), roleClaim.Value);
    }
}