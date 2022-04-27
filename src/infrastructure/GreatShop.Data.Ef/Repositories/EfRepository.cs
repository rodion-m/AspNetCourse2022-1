using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef.Repositories;

internal class EfRepository<TEntity> 
    : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<TEntity> _entities => _dbContext.Set<TEntity>();

    public async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _entities
            .FirstOrDefaultAsync(it => it.Id == id, cancellationToken: cancellationToken)
            ?? _entities.Local.First(it => it.Id == id);
    }

    public async Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _entities
                   .FirstOrDefaultAsync(it => it.Id == id, cancellationToken: cancellationToken)
               ?? _entities.Local.FirstOrDefault(it => it.Id == id);    }

    public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
    }

    public Task Update(TEntity TEntity, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(TEntity).State = EntityState.Modified;
        return Task.CompletedTask;
    }
}