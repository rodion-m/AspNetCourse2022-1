using Lesson14.Models;

namespace Lesson21.UoW.Data;

public interface IRepository<TEntity>
    where TEntity : IEntity
{
    Task<TEntity> GetById(Guid Id);
    Task<IReadOnlyList<TEntity>> GetAll();
    Task Add(TEntity entity);
    Task Update(TEntity entity);
}