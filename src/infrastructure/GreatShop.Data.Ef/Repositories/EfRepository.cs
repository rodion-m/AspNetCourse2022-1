using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef.Repositories;

internal abstract class EfRepository<TEntity> 
    : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext _dbContext;
    protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();


    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await Entities.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
            ?? Entities.Local.First(it => it.Id == id);
    }

    public virtual async Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        return await Entities.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
               ?? Entities.Local.FirstOrDefault(it => it.Id == id);    }

    public virtual async ValueTask Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual ValueTask Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        Entities.Update(entity);
        return ValueTask.CompletedTask;
    }
}