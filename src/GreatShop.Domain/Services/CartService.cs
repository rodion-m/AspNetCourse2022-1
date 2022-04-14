using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace GreatShop.Domain.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ILogger<CartService> _logger;

        public CartService(
            ICartRepository cartRepository, 
            ICartItemRepository cartItemRepository, 
            ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _logger = logger;
        }

        public async Task AddProduct(Guid accountId, Guid productId, int quantity = 1)
        {
            var cart = await _cartRepository.GetCartByAccountId(accountId);
            var cartItem = cart.Items.SingleOrDefault(i => i.ProductId == productId);

            if (cartItem is not null)
            {
                _logger.LogDebug("Adding quantity to an existed item");
                cartItem.Quantity += quantity;
                await _cartItemRepository.Update(cartItem);
            }
            else
            {
                _logger.LogDebug("Adding new item to cart");
                cartItem = new CartItem() { Id = Guid.NewGuid(), ProductId = productId, Quantity = quantity };
                cart.Items.Add(cartItem);
                await _cartItemRepository.Add(cartItem);
            }
            await _cartRepository.Update(cart);
        }

        public Task<Cart> GetAccountCart(Guid accountId)
        {
            _logger.LogDebug("Get cart for account: {AccountId}", accountId);
            return _cartRepository.GetCartByAccountId(accountId);
        }

    }
}
