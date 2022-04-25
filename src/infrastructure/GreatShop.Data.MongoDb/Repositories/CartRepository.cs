using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb.Repositories;


internal class CartRepository : ICartRepository
{
    private readonly IMongoCollection<Cart> _collection;
    private readonly IClientSessionHandle _session;

    public CartRepository(IMongoCollection<Cart> collection, IClientSessionHandle session)
    {
        _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public Task<Cart> GetById(Guid id)
    {
        return _collection.Find(_session, it => it.Id == id).SingleAsync();
    }

    public Task<Cart?> FindById(Guid id)
    {
        return _collection.Find(_session, it => it.Id == id).SingleOrDefaultAsync()!;
    }

    public Task Add(Cart entity)
    {
        return _collection.InsertOneAsync(_session, entity);
    }

    public Task Update(Cart entity)
    {
        return _collection.ReplaceOneAsync(_session, it => it.Id == entity.Id, entity);
    }

    public Task<Cart> GetCartByAccountId(Guid accountId)
    {
        return _collection.Find(_session, it => it.AccountId == accountId).SingleAsync();
    }

    public Task<Cart?> FindCartByAccountId(Guid accountId)
    {
        return _collection.Find(_session, it => it.AccountId == accountId).SingleOrDefaultAsync()!;
    }
}