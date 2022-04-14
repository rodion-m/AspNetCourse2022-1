using Lesson4_HttpModels;
using Refit;

namespace Lesson4_ApiClientRefit;

public interface IShopClient
{
    [Get("/products/all")]
    public Task<IReadOnlyList<Product>> GetAllProducts();

    [Post("/products/add")]
    public Task AddProduct(Product product);
}