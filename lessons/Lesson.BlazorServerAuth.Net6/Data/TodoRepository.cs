using Lesson.BlazorServerAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson.BlazorServerAuth.Data;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TodoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<TodoItem>> GetAllByUserId(Guid userId)
    {
        return await _dbContext.TodoItems.Where(it => it.UserId == userId).ToListAsync();
    }

    public async Task Remove(TodoItem item)
    {
        _dbContext.TodoItems.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Add(TodoItem item)
    {
        await _dbContext.TodoItems.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(TodoItem item)
    {
        _dbContext.Entry(item).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }


    public Task<TodoItem> GetById(Guid itemId) 
        => _dbContext.TodoItems.FirstAsync(it => it.Id == itemId);
}