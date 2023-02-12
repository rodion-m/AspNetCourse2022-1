using Lesson14.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson21.UoW.Data;

public class EfRepository<TEntity> 
    : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext _dbContext;
    protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();


    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual ValueTask Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Entities.Update(entity);
        return ValueTask.CompletedTask;
    }
}