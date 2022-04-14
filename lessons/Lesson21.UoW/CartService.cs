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

    public async Task AddItem(Guid accountId, Guid productId, double quantity = 1d)
    {
        var cart = await _uow.CartRepository.GetByAccountId(accountId);
        await AddItem(cart, productId, quantity);
    }

    public async Task AddItem(Cart cart, Guid productId, double quantity = 1d)
    {
        var existedItem = cart.Items.SingleOrDefault(it => it.ProductId == productId);
        if (existedItem is not null)
            existedItem.Quantity += quantity;
        else
            cart.Items.Add(existedItem);

        await _uow.CartRepository.Update(cart);
        await _uow.SaveChangesAsync();
    }
}