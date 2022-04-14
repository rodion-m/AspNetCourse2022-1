namespace Lesson15.Controllers.Models;

public class Account : IEntity
{
    public string? Email { get; set; }
    public Guid Id { get; init; }
}