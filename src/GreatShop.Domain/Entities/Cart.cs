namespace GreatShop.Domain.Entities;

public record Cart : IEntity
{
    public Cart() {}
    public Cart(Guid id, Guid accountId, List<CartItem> items)
    {
        Id = id;
        AccountId = accountId;
        Items = items;
    }

    public Guid Id { get; init; }
    public Guid AccountId { get; set; }
    public List<CartItem> Items { get; set; } = null!;
    
    public int ItemCount => Items.Count;
}

public record CartItem : IEntity
{
    public Guid Id { get; init; }
    
    public Guid ProductId { get; init; }
    public double Quantity { get; set; }
    
    public Cart Cart { get; set; } = null!;
}