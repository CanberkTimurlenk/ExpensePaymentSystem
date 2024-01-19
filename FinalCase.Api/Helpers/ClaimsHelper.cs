using FinalCase.Business.Features.Authentication.Constants.Jwt;
using System.Security.Claims;

namespace FinalCase.Api.Helpers;
public static class ClaimsHelper
{
    /// <summary>
    /// Get the user id from the claims if it exists
    /// </summary>
    /// <param name="identity">The claim identity</param>
    /// <param name="userId">The id will be obtained from the claim</param>
    /// <param name="claimType">Claim type</param>
    /// <returns>True if id is obtained, otherwise false</returns>
    public static bool TryGetUserIdFromClaims(ClaimsIdentity identity, out int userId, string claimType = JwtPayloadFields.Id)
    {
        userId = 0;

        if (identity == null)
            return false;

        var claim = identity.FindFirst(claimType);

        return claim != null && int.TryParse(claim.Value, out userId);
    }
}