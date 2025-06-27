using Microsoft.AspNetCore.Identity;

public static class PasswordHelper
{
    private static readonly PasswordHasher<object> hasher = new();

    public static string HashPassword(string password)
    {
        return hasher.HashPassword(null, password);
    }
    public static bool VerifyPassword(string hashedPassword, string inputPassword)
    {
        var result = hasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
        return result == PasswordVerificationResult.Success;
    }

}
