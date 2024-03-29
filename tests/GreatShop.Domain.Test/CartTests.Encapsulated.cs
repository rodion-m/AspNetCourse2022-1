﻿using System;
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

public class CartTests_Encapsulated
{
    private readonly ILogger<CartService> _logger;

    public CartTests_Encapsulated(ITestOutputHelper output)
    {
        //Divergic.Logging.XUnit
        _logger = output.BuildLoggerFor<CartService>();
    }
    
    [Fact]
    public void AddProduct_WithExistingProduct_AddsQuantity()
    {
        var productId = new Guid("1D42C5BE-96C3-4524-A4B5-46BE7B78C4EC");
        var cart = CreateCart(productId);
        
        cart.Add(CreateFakeProduct(productId), 1d);
        
        Assert.Single(cart.Items);
        var item = cart.Items.First();
        Assert.Equal(2d, item.Quantity);
    }

    private static Cart CreateCart(Guid productId)
    {
        return new Cart
        (
            id: new Guid("4077306B-904C-4D75-B77F-4BC731E5835F"),
            accountId: new Guid("283A4CDC-2979-4180-9821-A219D15E25E4"),
            items: new List<CartItem>()
            {
                new(new Guid("06E192E2-AD6B-4E5E-9B59-80ADCCD0C767"), productId, 1d)
            }
        );
    }

    private static Product CreateFakeProduct(Guid productId)
    {
        return new Product(productId, Guid.Empty, "", decimal.One, "https://site.com/");
    }
}