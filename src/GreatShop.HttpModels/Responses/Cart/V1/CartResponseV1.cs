using GreatShop.HttpModels.Responses.Common;

namespace GreatShop.HttpModels.Responses.Cart.V1;

public class CartResponseV1
{
    public IReadOnlyCollection<CartItemModel> ItemModels { get; }

    public CartResponseV1(IReadOnlyCollection<CartItemModel> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        ItemModels = items;
    }

    public record CartItemModel(ProductModelV1 Product, double Quantity);
}