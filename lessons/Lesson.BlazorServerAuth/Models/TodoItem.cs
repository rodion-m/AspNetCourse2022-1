namespace Lesson.BlazorServerAuth.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string Text { get; set; } = "";
    public bool IsDone { get; set; }
}