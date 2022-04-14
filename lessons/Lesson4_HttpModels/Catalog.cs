namespace Lesson4_HttpModels;

public interface ICatalog
{
    IEnumerable<Product> GetProducts();
    void AddProduct(Product product);
}

public class InMemoryCatalog : ICatalog
{
    private readonly Product[] _products =
    {
        new Product(Guid.NewGuid(), "Чистый код", 1000)
    };

    public IEnumerable<Product> GetProducts()
    {
        return _products;
    }

    public void AddProduct(Product product)
    {
        throw new NotImplementedException();
    }
}