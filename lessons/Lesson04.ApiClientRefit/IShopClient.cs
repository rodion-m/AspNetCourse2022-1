using Lesson04.HttpModels;
using Refit;

namespace Lesson04.ApiClientRefit;

public interface IShopClient
{
    [Get("/products/all")]
    public Task<IReadOnlyList<Product>> GetAllProducts();

    [Post("/products/add")]
    public Task AddProduct(Product product);
}