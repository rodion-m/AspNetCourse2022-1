using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface IRepository<TEntity> 
    where TEntity: IEntity
{
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken = default);
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
    Task Update(TEntity entity, CancellationToken cancellationToken = default);
}