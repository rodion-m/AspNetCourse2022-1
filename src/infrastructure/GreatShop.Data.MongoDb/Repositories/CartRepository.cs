using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb.Repositories;


internal class CartRepository : MongoGenericRepository<Cart>, ICartRepository
{
    public CartRepository(IMongoCollection<Cart> collection, IClientSessionHandle session)
    : base(collection, session)
    {
    }

    public Task<Cart> GetCartByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        return _collection.Find(_session, it => it.AccountId == accountId)
            .SingleAsync(cancellationToken: cancellationToken);
    }

    public Task<Cart?> FindCartByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        return _collection.Find(_session, it => it.AccountId == accountId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken)!;
    }
}