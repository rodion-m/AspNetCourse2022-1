using Lesson14.Models;
using Lesson21.UoW.Data;

namespace Lesson21.UoW;

public class CartService
{
    private readonly IUnitOfWork _uow;

    public CartService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Cart> GetCartForAccount(Guid accountId)
    {
        var cart = await _uow.CartRepository.GetByAccountId(accountId);
        return cart;
    }

    public async Task AddItem(Guid accountId, Product product, double quantity = 1d)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if(quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        var cart = await _uow.CartRepository.GetByAccountId(accountId);
        await AddItem(cart, product, quantity);
    }

    public async Task AddItem(Cart cart, Product product, double quantity = 1d)
    {
        if (cart == null) throw new ArgumentNullException(nameof(cart));
        if (product == null) throw new ArgumentNullException(nameof(product));
        if(quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        
        var existedItem = cart.Items.SingleOrDefault(it => it.ProductId == product.Id);
        if (existedItem is not null)
        {
            existedItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem()
            {
                ProductId = product.Id, Quantity = quantity, Price = product.Price
            });
        }

        await _uow.CartRepository.Update(cart);
        await _uow.SaveChangesAsync();
    }
}