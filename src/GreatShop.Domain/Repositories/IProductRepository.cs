using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> GetProducts(Guid categoryId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetAllProducts(CancellationToken cancellationToken = default);
}