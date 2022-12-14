using GreatShop.Domain.Entities;
using GreatShop.Domain.Exceptions;
using GreatShop.Domain.Repositories;

namespace GreatShop.Domain.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepo;
    private readonly IPasswordHasherService _hasher;
    private readonly ITokenService _tokenService;

    public AccountService(
        IPasswordHasherService hasher,
        ITokenService tokenService, 
        IAccountRepository accountRepo)
    {
        _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _accountRepo = accountRepo ?? throw new ArgumentNullException(nameof(accountRepo));
    }

    public virtual async Task<(Account account, string token)> Register(
        string email, string name, string password)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (password == null) throw new ArgumentNullException(nameof(password));
        //проверка уникальности email...

        Account account = new(
            id: Guid.NewGuid(), 
            name: name,
            passwordHash: _hasher.HashPassword(password),
            email: email,
            roles: Roles.Defaults.Customers
        );

        await _accountRepo.Add(account);
        var token = _tokenService.GenerateToken(account);
        return (account, token);
    }

    public virtual async Task<(Account account, string token)> LogIn(
        string email, string password)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));
        
        var account = await _accountRepo.FindByEmail(email);
        if (account is null)
        {
            throw new EmailNotFoundException(email);
        }

        if (!_hasher.VerifyPassword(account.PasswordHash, password))
        {
            throw new IncorrectPasswordException();
        }

        return (account, _tokenService.GenerateToken(account));
    }
}