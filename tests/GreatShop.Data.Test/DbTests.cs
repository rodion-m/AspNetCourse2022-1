using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GreatShop.Data.Ef;
using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Xunit;

namespace GreatShop.Data.Test;

public abstract partial class DbTests : IDisposable
{
    protected IUnitOfWorkFactory _unitOfWorkFactory = null!;
    static partial void LoadEnvironmentVariables();

    internal DbTests()
    {
        LoadEnvironmentVariables();
    }
    
    [Fact]
    public async Task Adding_item_to_a_cart_works()
    {
        await using var unitOfWork = await _unitOfWorkFactory.CreateAsync();
        var cartRepository = unitOfWork.CartRepository;
        var cartId = Guid.NewGuid();
        await cartRepository.Add(new Cart(cartId, Guid.NewGuid(), new List<CartItem>()
        {
            new(Guid.NewGuid(), Guid.NewGuid(), 1),
            new(Guid.NewGuid(), Guid.NewGuid(), 2),
        }));
        await unitOfWork.CommitAsync();
        var cart = await cartRepository.GetById(cartId);
        Assert.Equal(2, cart.ItemCount);
    }
    
    [Fact]
    public async Task Create_and_dispose_works()
    {
        await using (await _unitOfWorkFactory.CreateAsync())
        {
        }
    }
    
    [Fact]
    public async Task Add_account_and_cart_works()
    {
        var account = CreateAccount();
        var cart = CreateCart(account);
        await using (var uow = await _unitOfWorkFactory.CreateAsync())
        {
            await uow.AccountRepository.Add(account);
            await uow.CartRepository.Add(cart);
            await uow.CommitAsync();
        }
        
        await using (var uow = await _unitOfWorkFactory.CreateAsync())
        {
            var accountFromDb = await uow.AccountRepository.GetById(account.Id);
            var cartFromDb = await uow.CartRepository.GetById(cart.Id);
            Assert.Equal(account.Id, accountFromDb.Id);
            Assert.Equal(cart.Id, cartFromDb.Id);
        }
    }
    
    [Fact]
    public async Task Retrieve_document_from_local_session_works()
    {
        await using var uow = await _unitOfWorkFactory.CreateAsync();
        var account = CreateAccount();
        await uow.AccountRepository.Add(account);
        var accountFromDb = await uow.AccountRepository.GetById(account.Id);
        Assert.Equal(account.Id, accountFromDb.Id);
    }
    
    [Fact]
    public async Task Rollback_transaction_works()
    {
        var account = CreateAccount();
        await using (var uow = await _unitOfWorkFactory.CreateAsync())
        {
            await uow.AccountRepository.Add(account);
        }
        
        await using (var uow = await _unitOfWorkFactory.CreateAsync(false))
        {
            var accountFromDb = await uow.AccountRepository.FindById(account.Id);
            Assert.Null(accountFromDb);
        }
    }
    
    [Fact]
    public async Task Cancel_document_adding_works()
    {
        if (this is EfTests)
        {
            Assert.True(true, "Document adding cancellation in EF skipped since it's not supported.");
            return;
        }
        var account = CreateAccount() with { Name = new string(' ', 1_000_000)};
        {
            var cts = new CancellationTokenSource();
            await using var uow = await _unitOfWorkFactory.CreateAsync(cancellationToken: cts.Token);
            await uow.AccountRepository.Add(account with{Id = Guid.NewGuid()}, cts.Token);
            var task = Task.Run(() => uow.AccountRepository.Add(account, cts.Token));
            cts.Cancel();
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => task);
        }

        await using (var uow = await _unitOfWorkFactory.CreateAsync(false))
        {
            var accountFromDb = await uow.AccountRepository.FindById(account.Id);
            Assert.Null(accountFromDb);
        }
    }
    
    [Fact]
    public async Task Cancel_transaction_commiting_works()
    {
        var account = CreateAccount();
        {
            await using var uow = await _unitOfWorkFactory.CreateAsync();
            await uow.AccountRepository.Add(account);

            var cts = new CancellationTokenSource();
            var task = Task.Run(async () => await uow.CommitAsync(cts.Token));
            cts.Cancel();
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => task);
        }

        await using (var uow = await _unitOfWorkFactory.CreateAsync(false))
        {
            var accountFromDb = await uow.AccountRepository.FindById(account.Id);
            Assert.Null(accountFromDb);
        }
    }

    private static Cart CreateCart(Account account)
    {
        return new Cart(Guid.NewGuid(), account.Id, new List<CartItem>());
    }

    private static Account CreateAccount()
    {
        return new Account(Guid.NewGuid(), "Name", "asd@asd.com", "", new[] { "Admin" });
    }

    public abstract void Dispose();
}