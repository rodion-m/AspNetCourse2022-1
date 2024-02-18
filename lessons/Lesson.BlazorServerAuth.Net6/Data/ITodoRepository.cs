using Lesson.BlazorServerAuth.Models;

namespace Lesson.BlazorServerAuth.Data;

public interface ITodoRepository
{
    Task<TodoItem> GetById(Guid itemId);
    Task<IReadOnlyCollection<TodoItem>> GetAllByUserId(Guid userId);
    Task Add(TodoItem item);
    Task Update(TodoItem item);
    Task Remove(TodoItem item);
}