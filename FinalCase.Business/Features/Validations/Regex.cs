namespace FinalCase.Business.Features.Validations;

public static class Regex
{
    public const string PasswordRegex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";
    public const string ValidUrlRegex = @"https ?:\/\/ (www\.)?[-a - zA - Z0 - 9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)\";
}
