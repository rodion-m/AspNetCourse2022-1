using Lesson14.Models;
using Lesson19.Auth.Data;
using Microsoft.AspNetCore.Identity;

namespace Lesson19.Auth.Services;

public class AuthService
{
    private readonly AccountRepository _accountRepo;
    private readonly IPasswordHasher<Account> _hasher;
    private readonly ITokenService _tokenService;

    public AuthService(
        IPasswordHasher<Account> hasher,
        ITokenService tokenService,
        AccountRepository accountRepo)
    {
        _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _accountRepo = accountRepo ?? throw new ArgumentNullException(nameof(accountRepo));
    }

    public async Task<(Account acc, string token)> Register(
        string email, string name, string password)
    {
        //проверка уникальности email...

        Account account = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email
        };

        account.PasswordHash = _hasher.HashPassword(account, password);

        //await _accountRepository.Add(account);
        var token = _tokenService.GenerateToken(account);
        return (account, token);
    }

    public async Task<(Account acc, string token)> LogIn(string email, string password)
    {
        var account = await _accountRepo.FindByEmail(email);
        if (account is null) throw new EmailNotFoundException(email);

        var result = _hasher.VerifyHashedPassword(
            account, account.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new IncorrectPasswordException();
        }

        return (account, _tokenService.GenerateToken(account));
    }
}

public class EmailNotFoundException : Exception
{
    public EmailNotFoundException(string email)
    {
        Email = email;
    }

    public string Email { get; }
}

public class IncorrectPasswordException : Exception
{
}