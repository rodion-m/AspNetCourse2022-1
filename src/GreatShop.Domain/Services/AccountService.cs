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
        string email, string name, string password, CancellationToken cancellationToken)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (password == null) throw new ArgumentNullException(nameof(password));
        
        if(await _accountRepo.IsAccountExist(email, cancellationToken))
        {
            throw new AccountEmailAlreadyExistsException(email);
        }

        Account account = new(
            id: Guid.NewGuid(), 
            name: name,
            passwordHash: _hasher.HashPassword(password),
            email: email,
            roles: new []{ Role.Customer }
        );
        await _accountRepo.Add(account, cancellationToken);
        var token = _tokenService.GenerateToken(account);
        return (account, token);
    }

    public virtual async Task<(Account account, string token)> LogIn(
        string email, string password, CancellationToken cancellationToken)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));
        
        var account = await _accountRepo.FindByEmail(email, cancellationToken);
        if (account is null)
        {
            throw new EmailNotFoundException(email);
        }
        ThrowIfPasswordDoesntMatch(password, account.PasswordHash);

        return (account, _tokenService.GenerateToken(account));
    }

    private void ThrowIfPasswordDoesntMatch(string passwordHash, string providedPassword)
    {
        if (providedPassword == null) throw new ArgumentNullException(nameof(providedPassword));
        if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
        if (!_hasher.VerifyPassword(passwordHash, providedPassword))
        {
            throw new IncorrectPasswordException();
        }
    }
}