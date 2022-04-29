using Lesson14.Models;

namespace Lesson21.UoW.Data;

public interface IRepository<TEntity> where TEntity: IEntity
{
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken = default);
    ValueTask Add(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask Update(TEntity entity, CancellationToken cancellationToken = default);
}