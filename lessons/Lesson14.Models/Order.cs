#pragma warning disable CS8618
namespace Lesson14.Models;

public class Order : IEntity
{
    public string Phone { get; set; } // GET И SET
    public decimal TotalPrice { get; set; } // GET И SET
    public OrderStatus Status { get; set; }
    public Guid Id { get; init; }
}

public enum OrderStatus
{
    Created,
    Offered
}