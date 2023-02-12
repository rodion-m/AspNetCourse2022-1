namespace Lesson14.Models;

public class Product : IEntity
{
    public Guid Id { get; init; }
    public decimal Price { get; init; }
}