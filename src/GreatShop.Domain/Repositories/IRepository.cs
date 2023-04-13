using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface IRepository<TEntity> 
    where TEntity: IEntity
{
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken = default);
    ValueTask Add(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask Update(TEntity entity, CancellationToken cancellationToken = default);
    
    Guid NewGuid();
}