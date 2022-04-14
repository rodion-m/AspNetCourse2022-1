namespace GreatShop.Domain.Entities;

public record Product : IEntity
{
    public Product(Guid id, Guid categoryId, string name, decimal price, string imageUri)
    {
        Id = id;
        CategoryId = categoryId;
        Name = name;
        Price = price;
        ImageUri = imageUri;
    }

    public Guid Id { get; init; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUri { get; set; }
}