using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<TEntity> _entities => _dbContext.Set<TEntity>();

    public Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
        => _entities.FirstAsync(it => it.Id == id, cancellationToken: cancellationToken);

    public Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken = default) 
        => _entities.FirstOrDefaultAsync(it => it.Id == id, cancellationToken: cancellationToken);

    public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(TEntity TEntity, CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}