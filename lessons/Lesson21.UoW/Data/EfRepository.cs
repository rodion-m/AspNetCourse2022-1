using Lesson14.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson21.UoW.Data;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> _entities => _dbContext.Set<TEntity>();

    public Task<TEntity> GetById(Guid Id)
    {
        return _entities.FirstAsync(it => it.Id == Id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public async Task Add(TEntity entity)
    {
        await _entities.AddAsync(entity);
        //await _dbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity TEntity)
    {
        _dbContext.Entry(TEntity).State = EntityState.Modified;
        //await _dbContext.SaveChangesAsync();
    }
}