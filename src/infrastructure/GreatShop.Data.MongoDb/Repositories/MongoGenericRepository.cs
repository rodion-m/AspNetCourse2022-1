using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb.Repositories;

internal class MongoGenericRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
{
    protected readonly IMongoCollection<TEntity> _collection;
    protected readonly IClientSessionHandle _session;

    public MongoGenericRepository(IMongoCollection<TEntity> collection, IClientSessionHandle session)
    {
        _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return _collection.Find(_session, it => it.Id == id)
            .SingleAsync(cancellationToken: cancellationToken);
    }

    public Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        return _collection.Find(_session, it => it.Id == id)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken)!;
    }

    public Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        return _collection.InsertOneAsync(_session, entity, cancellationToken: cancellationToken);
    }

    public Task Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        return _collection.ReplaceOneAsync(
            _session, it => it.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }
}