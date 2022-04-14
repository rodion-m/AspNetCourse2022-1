using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using GreatShop.Domain.Services;
using GreatShop.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace GreatShop.Domain.Test;

public class CartServiceTests
{
    private readonly ILogger<CartService> _logger;

    public CartServiceTests(ITestOutputHelper output)
    {
        _logger = output.BuildLoggerFor<CartService>();
    }

    [Fact]
    public async Task New_item_added_to_cart()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;
        
        await using var context = new AppDbContext(options);
        await context.Database.EnsureCreatedAsync();
        
        ICartRepository cartRepo = new CartRepository(context);
        ICartItemRepository cartItemRepo = new CartItemRepository(context);
        var accountId = Guid.NewGuid();
        await cartRepo.Add(
            new Cart(Guid.NewGuid(), accountId, new List<CartItem>())
        );
        
        var cartService = new CartService(cartRepo, cartItemRepo, _logger);
        
        var productId = Guid.NewGuid();
        await cartService.AddProduct(accountId, productId);
        var cart = await cartService.GetAccountCart(accountId);
        Assert.Single(cart.Items);
        var item = cart.Items[0];
        Assert.Equal(productId, item.ProductId);
        Assert.Equal(1d, item.Quantity);
    }
}