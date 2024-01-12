using System.Security.Cryptography;

namespace FinalCase.Base.Helpers.Encryption;
public static class Md5Extension
{
    public static string Create(string input)
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes).ToLower();
    }
    public static string GetHash(string input)
    {
        var hash = Create(input);
        return Create(hash);
    }
}