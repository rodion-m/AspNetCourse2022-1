using Lesson14.Models;
using Lesson21.UoW.Data;
using Microsoft.AspNetCore.Identity;

namespace Lesson21.UoW;

public class AuthServiceUnsafe
{
    private readonly IAccountRepository _accountRepo;
    private readonly ICartRepository _cartRepo;
    private readonly IPasswordHasher<Account> _hasher;

    public AuthServiceUnsafe(IPasswordHasher<Account> hasher, IAccountRepository accountRepo, ICartRepository cartRepo)
    {
        _hasher = hasher;
        _accountRepo = accountRepo;
        _cartRepo = cartRepo;
    }

    public async Task<Account> Register(string email, string name, string password)
    {
        Account account = new() { Id = Guid.NewGuid(), Name = name, Email = email };
        Cart cart = new() { Id = Guid.NewGuid(), AccountId = account.Id };

        await _accountRepo.Add(account);
        await _cartRepo.Add(cart);

        return account;
    }
}