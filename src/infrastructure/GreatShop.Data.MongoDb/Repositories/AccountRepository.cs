using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb.Repositories;

//TODO Generic Repo
internal class AccountRepository : IAccountRepository
{
    private readonly IMongoCollection<Account> _collection;
    private readonly IClientSessionHandle _session;

    public AccountRepository(IMongoCollection<Account> collection, IClientSessionHandle session)
    {
        _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public Task<Account> GetById(Guid id)
    {
        return _collection.Find(_session, it => it.Id == id).SingleAsync();
    }

    public Task<Account?> FindById(Guid id)
    {
        return _collection.Find(_session, it => it.Id == id).SingleOrDefaultAsync()!;
    }

    public Task Add(Account entity)
    {
        return _collection.InsertOneAsync(_session, entity);
    }

    public Task Update(Account entity)
    {
        return _collection.ReplaceOneAsync(_session, it => it.Id == entity.Id, entity);
    }
}