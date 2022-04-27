using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb.Repositories;

internal class ProductRepository : MongoGenericRepository<Product>, IProductRepository
{
    public ProductRepository(IMongoCollection<Product> collection, IClientSessionHandle session) 
        : base(collection, session)
    {
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_session, it => it.CategoryId == categoryId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Product>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_session, _ => true)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}