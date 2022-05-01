using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;

namespace GreatShop.Domain.Services;

public class CatalogService
{
    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly IClock _clock;

    public CatalogService(IUnitOfWorkFactory uowFactory, IClock clock)
    {
        _uowFactory = uowFactory;
        _clock = clock;
    }

    public async Task<IReadOnlyList<Product>> GetProducts(Guid categoryId)
    {
        await using var uow = await _uowFactory.CreateAsync();
        var products = await uow.ProductRepository.GetProducts(categoryId);
        if (_clock.GetCurrentTime().DayOfWeek == DayOfWeek.Sunday)
        {
            return products.Select(it => it with {Price = it.Price * 1.1m}).ToArray();
        }

        return products;
    }

    public async Task<Product> GetProduct(Guid productId)
    {
        await using var uow = await _uowFactory.CreateAsync();
        return await uow.ProductRepository.GetById(productId);
    }

    public async Task<IReadOnlyList<Product>> GetAllProducts()
    {
        await using var uow = await _uowFactory.CreateAsync();
        return await uow.ProductRepository.GetAllProducts();
    }
}