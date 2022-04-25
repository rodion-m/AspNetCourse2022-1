using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GreatShop.Domain.Entities;
using Xunit;

namespace GreatShop.Data.MongoDb.Test;

public partial class UnitOfWorkFactoryTests
{
    private readonly UnitOfWorkFactory _unitOfWorkFactory;
    partial void LoadEnvironmentVariables();
    
    public UnitOfWorkFactoryTests()
    {
        LoadEnvironmentVariables();
        var connectionString = Environment.GetEnvironmentVariable("mongodb_connection_string")!;
        ArgumentNullException.ThrowIfNull(connectionString);
        _unitOfWorkFactory = new UnitOfWorkFactory(connectionString, "db_test");
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
            await Task.Run(async () => await uow.CommitAsync());
            //await uow.CommitAsync();
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
        var account = CreateAccount() with { Name = new string(' ', 1_000_000)};
        {
            var cts = new CancellationTokenSource();
            await using var uow = await _unitOfWorkFactory.CreateAsync(cancellationToken: cts.Token);
            var task = uow.AccountRepository.Add(account, cts.Token);
            cts.Cancel();
            await Assert.ThrowsAsync<OperationCanceledException>(() => task);
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
            await Assert.ThrowsAsync<OperationCanceledException>(() => task);
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
}