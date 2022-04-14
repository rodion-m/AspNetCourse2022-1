#pragma warning disable CS8618
namespace Lesson15.Controllers.Models;

public class Order : IEntity
{
    public string Phone { get; set; } // GET И SET
    public decimal TotalPrice { get; set; } // GET И SET
    public Guid Id { get; init; } // GET И SET
}