using FinalCase.Base.Token;
using FinalCase.Business.Features.ApplicationUsers.Authentication.Constants.Jwt;
using FinalCase.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalCase.Business.Features.ApplicationUsers.Authentication.Helpers;
public static class TokenHelper
{
    public static string CreateAccessToken(ApplicationUser user, JwtConfig jwtConfig)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken); // Access Token        
    }

    private static Claim[] GetClaims(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(JwtPayloadFields.Id, user.Id.ToString()),
            new Claim(JwtPayloadFields.Email, user.Email),
            new Claim(JwtPayloadFields.Username, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };
        return claims;
    }
}
