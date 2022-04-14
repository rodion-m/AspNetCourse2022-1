using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface IRepository<TEntity> 
    where TEntity: IEntity
{
    Task<TEntity> GetById(Guid Id);
    Task<TEntity?> FindById(Guid Id);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
}