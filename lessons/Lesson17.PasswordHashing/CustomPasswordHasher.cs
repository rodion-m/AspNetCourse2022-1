using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Lesson17.PasswordHashing;

public class CustomPasswordHasher
{
    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8);
        var bytes = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            10_000,
            256 / 8);
        var hashed = Convert.ToBase64String(bytes);
        return hashed;
    }

    private void Example()
    {
        var password = "";
        var user = new User();
        user.PasswordHash = HashPassword(password);
    }
}