using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef.Repositories;

internal class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts(
        Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await Entities.Where(it => it.CategoryId == categoryId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Product>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        return await Entities
            .ToListAsync(cancellationToken: cancellationToken);
    }
}