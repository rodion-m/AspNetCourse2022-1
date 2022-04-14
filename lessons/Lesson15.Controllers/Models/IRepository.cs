namespace Lesson15.Controllers.Models;

public interface IRepository<TEntity>
    where TEntity : IEntity
{
    Task<TEntity> GetById(Guid Id);
    Task<IReadOnlyList<TEntity>> GetAll();
    Task Add(TEntity entity);
    Task Update(TEntity entity);
}