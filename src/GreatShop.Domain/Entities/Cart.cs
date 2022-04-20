namespace GreatShop.Domain.Entities;

public record Cart : IEntity
{
    protected Cart()
    {
        _items = new List<CartItem>();
    }
    public Cart(Guid id, Guid accountId, List<CartItem> items)
    {
        Id = id;
        AccountId = accountId;
        _items = items;
    }

    public Guid Id { get; init; }
    public Guid AccountId { get; set; }
    
    private readonly List<CartItem> _items;
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    
    public int ItemCount => Items.Count;
    
    public void Add(Product product, double quantity = 1d)
    {
        var cartItem = Items.SingleOrDefault(it => it.ProductId == product.Id);
        if (cartItem is not null)
        {
            var newQty = cartItem.Quantity + quantity;
            if (newQty > 1000)
            {
                throw new InvalidOperationException("Quantity cannot be greater than 1000");
            }
            cartItem.Quantity = newQty;
        }
        else
        {
            cartItem = new CartItem() { Id = Guid.Empty, ProductId = product.Id, Quantity = quantity };
            _items.Add(cartItem);
        }
    }

}

public record CartItem : IEntity
{
    public Guid Id { get; init; }
    
    public Guid ProductId { get; init; }
    public double Quantity { get; set; }
    
    public Cart Cart { get; set; } = null!;
}