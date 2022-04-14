namespace Lesson14.Models;

public class Cart : IEntity
{
    public int ItemCount => Items.Count;
    public Guid AccountId { get; set; }

    public List<CartItem> Items { get; set; }
    public Guid Id { get; init; }

    public decimal GetTotalPrice()
    {
        return Items.Sum(it => it.Price);
    }
}

public class CartItem
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }
}