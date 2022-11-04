namespace Lesson04.HttpModels;

public interface ICatalog
{
    string GetProducts();
    void AddProduct(Product product);
}

public class InMemoryCatalog : ICatalog
{
    private readonly Product[] _products =
    {
        new Product(Guid.NewGuid(), "Чистый код", 1000)
    };

    public string GetProducts()
    {
        
        return _products;
    }

    // public (bool success, int number) AddProduct1(Product product)
    // {
    //     return (true, 0);
    // }
    public void AddProduct(Product product)
    {
        if (int.TryParse("42", out var number))
        {
            var result = number * 2;
            //...
        }
        else
        {
            //парсинг неуспешен
        }
        
        throw new NotImplementedException();
    }
}