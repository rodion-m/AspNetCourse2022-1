using GreatShop.Domain.Entities;
using GreatShop.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace GreatShop.WebApi.Services;

public class Pbkdf2PasswordHasher : IPasswordHasherService
{
    private readonly PasswordHasher<Account> _passwordHasher;
    private readonly Account _dummy = new(Guid.Empty, "", "fake@fake.com", "", Array.Empty<string>());

    public Pbkdf2PasswordHasher(IOptions<PasswordHasherOptions> optionsAccessor)
    {
        _passwordHasher = new PasswordHasher<Account>(optionsAccessor);
    }
    
    public string HashPassword(string password)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));
        var hashedPassword = _passwordHasher.HashPassword(_dummy, password);
        return hashedPassword;
    }

    public bool VerifyPassword(string passwordHash, string providedPassword)
    {
        if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
        if (providedPassword == null) throw new ArgumentNullException(nameof(providedPassword));
        var result = _passwordHasher
            .VerifyHashedPassword(_dummy, passwordHash, providedPassword);
        return result != PasswordVerificationResult.Failed;
    }
}