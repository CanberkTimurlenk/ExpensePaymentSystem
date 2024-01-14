namespace FinalCase.Business.Features.ApplicationUsers.Authentication.Constants.Jwt;

public static class JwtPayloadFields
{
    // created to prevent magic strings
    // if values changed somehow, we can change here and it will be reflected everywhere
    // if the value deleted, then it will be a compile time error. So, we can't forget to change it everywhere
    public const string Id = "Id";
    public const string Email = "Email";
    public const string Username = "Username";
}