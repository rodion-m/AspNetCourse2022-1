using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatShop.Data.Ef;
using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using GreatShop.Domain.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace GreatShop.Domain.Test;

public class CartServiceTests_SQLiteInMemory
{
    private readonly ILogger<CartService> _logger;

    public CartServiceTests_SQLiteInMemory(ITestOutputHelper output)
    {
        //Divergic.Logging.XUnit
        _logger = output.BuildLoggerFor<CartService>();
    }

    [Fact]
    public async Task New_item_added_to_cart()
    {
        await using var services = await TestServices.Create(_logger);
        var accountId = Guid.NewGuid();
        await services.CartRepository.Add(
            new Cart(Guid.NewGuid(), accountId, new List<CartItem>())
        );
        var productId = Guid.NewGuid();
        var product = CreateFakeProduct(productId);
        await services.CartService.AddProduct(accountId, product, 1d);
        var cart = await services.CartService.GetAccountCart(accountId);
        Assert.Single(cart.Items);
        var item = cart.Items.First();
        Assert.Equal(productId, item.ProductId);
        Assert.Equal(1d, item.Quantity);
    }

    private static Product CreateFakeProduct(Guid productId)
    {
        return new Product(productId, Guid.Empty, "", decimal.One, "https://site.com/");
    }

    [Fact]
    public async Task Quantity_of_item_added_to_cart_double_times_is_2()
    {
        await using var services = await TestServices.Create(_logger);
        var accountId = Guid.NewGuid();
        await services.CartRepository.Add(
            new Cart(Guid.NewGuid(), accountId, new List<CartItem>())
        );
        var productId = Guid.NewGuid();
        var product = CreateFakeProduct(productId);
        await services.CartService.AddProduct(accountId, product);
        await services.CartService.AddProduct(accountId, product);
        var cart = await services.CartService.GetAccountCart(accountId);
        Assert.Single(cart.Items);
        var item = cart.Items.First();
        Assert.Equal(productId, item.ProductId);
        Assert.Equal(2d, item.Quantity);
    }
    
    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(1000)]
    public async Task Quantity_of_item_added_to_cart_double_times_is_n(int n)
    {
        await using var services = await TestServices.Create(_logger);
        var accountId = Guid.NewGuid();
        await services.CartRepository.Add(
            new Cart(Guid.NewGuid(), accountId, new List<CartItem>())
        );
        var productId = Guid.NewGuid();
        var product = CreateFakeProduct(productId);
        for (int i = 0; i < n; i++)
        {
            await services.CartService.AddProduct(accountId, product);
        }

        var cart = await services.CartService.GetAccountCart(accountId);
        Assert.Single(cart.Items);
        var item = cart.Items.First();
        Assert.Equal(productId, item.ProductId);
        Assert.Equal((double)n, item.Quantity);
    }

    [Fact]
    public async Task Adding_item_to_cart_with_quantity_greater_than_1000_failed()
    {
        await using var services = await TestServices.Create(_logger);
        var accountId = Guid.NewGuid();
        await services.CartRepository.Add(
            new Cart(Guid.NewGuid(), accountId, new List<CartItem>())
        );
        var productId = Guid.NewGuid();
        var product = CreateFakeProduct(productId);
        await services.CartService.AddProduct(accountId, product, 500);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => services.CartService.AddProduct(accountId, product, 501)
        );
    }

    private class TestServices : IDisposable, IAsyncDisposable
    {
        public ICartRepository CartRepository { get; }
        public CartService CartService { get; }
        public AppDbContext DbContext { get; }
        private readonly SqliteConnection _connection;

        private TestServices(
            ICartRepository cartRepository,
            CartService cartService,
            AppDbContext dbContext,
            SqliteConnection connection)
        {
            CartRepository = cartRepository;
            CartService = cartService;
            DbContext = dbContext;
            _connection = connection;
        }

        public static async Task<TestServices> Create(ILogger<CartService> logger)
        {
            var (context, connection) = await CreateAppDbContext();
            ICartRepository cartRepo = new CartRepository(context);
            //ICartItemRepository cartItemRepo = new CartItemRepository(context);
            var cartService = new CartService(cartRepo, logger);
            return new TestServices(cartRepo, cartService, context, connection);
        }

        private static async Task<(
            AppDbContext context,
            SqliteConnection connection)> CreateAppDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            await connection.OpenAsync();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return (context, connection);
        }

        public void Dispose()
        {
            DbContext.Dispose();
            _connection.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await DbContext.DisposeAsync();
            await _connection.DisposeAsync();
        }
    }
}