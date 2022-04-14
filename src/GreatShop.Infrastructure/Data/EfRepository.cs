using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Infrastructure.Data;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<TEntity> _entities => _dbContext.Set<TEntity>();

    public Task<TEntity> GetById(Guid Id)
        => _entities.FirstAsync(it => it.Id == Id);

    public Task<TEntity?> FindById(Guid Id)
        => _entities.FirstOrDefaultAsync(it => it.Id == Id);

    public async Task Add(TEntity entity)
    {
        await _entities.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity TEntity)
    {
        _dbContext.Entry(TEntity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}