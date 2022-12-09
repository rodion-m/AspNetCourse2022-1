using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef.Repositories;

internal class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IReadOnlyList<Product>> GetProducts(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        return await Entities.Where(it => it.CategoryId == categoryId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetAllProducts(
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .ToListAsync(cancellationToken: cancellationToken);
    }
}