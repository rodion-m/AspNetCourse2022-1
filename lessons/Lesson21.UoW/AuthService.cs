using Lesson14.Models;
using Lesson21.UoW.Data;
using Microsoft.AspNetCore.Identity;

namespace Lesson21.UoW;

public class AuthService
{
    private readonly IPasswordHasher<Account> _hasher;
    private readonly IUnitOfWork _uow;

    public AuthService(IPasswordHasher<Account> hasher, IUnitOfWork uow)
    {
        _hasher = hasher;
        _uow = uow;
    }

    public async Task<Account> Register(string email, string name)
    {
        Account account = new() { Id = Guid.NewGuid(), Name = name, Email = email };
        Cart cart = new() { Id = Guid.NewGuid(), AccountId = account.Id };

        await _uow.AccountRepository.Add(account);
        await _uow.CartRepository.Add(cart);
        await _uow.SaveChangesAsync();

        return account;
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