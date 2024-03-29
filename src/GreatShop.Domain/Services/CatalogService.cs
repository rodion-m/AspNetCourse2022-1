﻿using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;

namespace GreatShop.Domain.Services;

public class CatalogService //Port (hexagonal architecture)
{
    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly IClock _clock;

    public CatalogService(IUnitOfWorkFactory uowFactory, IClock clock)
    {
        _uowFactory = uowFactory ?? throw new ArgumentNullException(nameof(uowFactory));
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    public virtual async Task<IReadOnlyList<Product>> GetProducts(Guid categoryId)
    {
        await using var uow = await _uowFactory.CreateAsync();
        var products = await uow.ProductRepository.GetProducts(categoryId);
        if (_clock.GetCurrentTime().DayOfWeek == DayOfWeek.Sunday)
        {
            return products.Select(it => it with {Price = it.Price * 1.1m}).ToArray();
        }

        return products;
    }

    public virtual async Task<Product> GetProduct(Guid productId)
    {
        await using var uow = await _uowFactory.CreateAsync();
        return await uow.ProductRepository.GetById(productId);
    }

    public virtual async Task<IReadOnlyList<Product>> GetAllProducts()
    {
        await using var uow = await _uowFactory.CreateAsync();
        return await uow.ProductRepository.GetAllProducts();
    }

    public async Task<Product> AddProduct(string name, Guid categoryId, decimal price, string imageUri)
    {
        if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(imageUri);
        await using var uow = await _uowFactory.CreateAsync();
        var product = new Product(uow.ProductRepository.NewGuid(), categoryId, name, price, imageUri);
        await uow.ProductRepository.Add(product);
        await uow.CommitAsync();
        return product;
    }
}