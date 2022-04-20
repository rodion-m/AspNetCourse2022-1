using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using GreatShop.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace GreatShop.Domain.Test;

public class CartTests_MockRepositories
{
    private readonly ILogger<CartService> _logger;

    public CartTests_MockRepositories(ITestOutputHelper output)
    {
        //Divergic.Logging.XUnit
        _logger = output.BuildLoggerFor<CartService>();
    }
    
    [Fact]
    public async Task AddProduct_WithExistingProduct_AddsQuantity()
    {
        var accountId = new Guid("283A4CDC-2979-4180-9821-A219D15E25E4");
        var productId = new Guid("1D42C5BE-96C3-4524-A4B5-46BE7B78C4EC");
        var cartRepositoryMock = new Mock<ICartRepository>();
        var cartItemsRepositoryMock = new Mock<ICartItemRepository>();
        var cart = new Cart
        (
            id: new Guid("4077306B-904C-4D75-B77F-4BC731E5835F"),
            accountId: accountId,
            items: new List<CartItem>()
            {
                new()
                {
                    Id = new Guid("06E192E2-AD6B-4E5E-9B59-80ADCCD0C767"),
                    ProductId = productId,
                    Quantity = 1d,
                }
            }
        );
        cartRepositoryMock.Setup(x => x.GetCartByAccountId(accountId))
            .ReturnsAsync(cart);
        cartRepositoryMock.Setup(x => x.Update(It.IsAny<Cart>()));
        cartItemsRepositoryMock.Setup(x => x.Update(It.IsAny<CartItem>()));

        var cartService = new CartService(cartRepositoryMock.Object, _logger);
        await cartService.AddProduct(accountId, CreateFakeProduct(productId), 1d);
        
        Assert.Single(cart.Items);
        var item = cart.Items.First();
        Assert.Equal(productId, item.ProductId);
        Assert.Equal(2d, item.Quantity);
    }

    private static Product CreateFakeProduct(Guid productId)
    {
        return new Product(productId, Guid.Empty, "", decimal.One, "https://site.com/");
    }
}