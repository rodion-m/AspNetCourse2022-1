using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface IRepository<TEntity> 
    where TEntity: IEntity
{
    Task<TEntity> GetById(Guid id);
    Task<TEntity?> FindById(Guid id);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
}