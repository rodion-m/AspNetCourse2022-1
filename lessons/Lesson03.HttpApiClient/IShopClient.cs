using Lesson04.HttpModels;

namespace Lesson03.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyList<Product>> GetProducts();
    Task AddProduct(Product product);
}