using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb.Repositories;

internal abstract class MongoGenericRepository<TEntity> 
    : IRepository<TEntity> where TEntity : IEntity
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly IClientSessionHandle Session;

    protected MongoGenericRepository(IMongoCollection<TEntity> collection, IClientSessionHandle session)
    {
        Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        Session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public virtual Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return Collection.Find(Session, it => it.Id == id)
            .SingleAsync(cancellationToken: cancellationToken);
    }

    public virtual Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        return Collection.Find(Session, it => it.Id == id)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken)!;
    }

    public virtual async ValueTask Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Collection.InsertOneAsync(Session, entity, cancellationToken: cancellationToken);
    }

    public virtual async ValueTask Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Collection.ReplaceOneAsync(
            Session, it => it.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }
}