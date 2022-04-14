using System.ComponentModel.DataAnnotations;

namespace GreatShop.HttpModels.Requests.Cart.V1;

public class UpdateCartRequestV1
{
    [Required] public IReadOnlyList<CartItemModel> Items { get; set; }

    public class CartItemModel
    {
        [Required] public Guid ProductId { get; set; }
        [Required] public double Quantity { get; set; }
    }
}