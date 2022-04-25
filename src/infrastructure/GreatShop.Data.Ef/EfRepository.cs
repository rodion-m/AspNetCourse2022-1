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

    public Task<TEntity> GetById(Guid id)
        => _entities.FirstAsync(it => it.Id == id);

    public Task<TEntity?> FindById(Guid id) 
        => _entities.FirstOrDefaultAsync(it => it.Id == id);

    public async Task Add(TEntity entity)
    {
        
        await _entities.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity TEntity)
    {
        await _dbContext.SaveChangesAsync();
    }
}