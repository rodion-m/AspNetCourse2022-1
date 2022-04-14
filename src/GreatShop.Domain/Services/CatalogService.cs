using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;

namespace GreatShop.Domain.Services;

public class CatalogService
{
    private readonly IProductRepository _productRepository;
    private readonly IClock _clock;

    public CatalogService(IProductRepository productRepository, IClock clock)
    {
        _productRepository = productRepository;
        _clock = clock;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts(Guid categoryId)
    {
        var products = await _productRepository.GetProducts(categoryId);
        if (_clock.GetCurrentTime().DayOfWeek == DayOfWeek.Sunday)
        {
            return products.Select(it => it with {Price = it.Price * 1.1m}).ToArray();
        }

        return products;
    }

    public Task<Product> GetProduct(Guid productId) 
        => _productRepository.GetById(productId);
}