using Lesson14.Models;
using Lesson19.Auth.Data;
using Microsoft.AspNetCore.Identity;

namespace Lesson19.Auth.Services;

public class AuthService
{
    private readonly AccountRepository _accountRepo;
    private readonly IPasswordHasher<Account> _hasher;
    private readonly ITokenService _tokenService;

    public AuthService(IPasswordHasher<Account> hasher, ITokenService tokenService, AccountRepository accountRepo)
    {
        _hasher = hasher;
        _tokenService = tokenService;
        _accountRepo = accountRepo;
    }

    public async Task<(Account acc, string token)> Register(string email, string name, string password)
    {
        //проверка уникальности email...

        Account account = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email
        };

        var hashedPassword = _hasher.HashPassword(account, password);
        account.PasswordHash = hashedPassword;

        //await _accountRepository.Add(account);
        var token = _tokenService.GenerateToken(account);
        return (account, token);
    }

    public async Task<(Account acc, string token)> LogIn(string email, string password)
    {
        var account = await _accountRepo.FindByEmail(email);
        if (account == null) throw new EmailNotFoundException(email);

        if (_hasher.VerifyHashedPassword(account, account.PasswordHash, password)
            == PasswordVerificationResult.Failed)
            throw new IncorrectPasswordException();

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