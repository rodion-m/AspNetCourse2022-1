using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyCollection<Product>> GetProducts(Guid categoryId, CancellationToken cancellationToken = default);
}