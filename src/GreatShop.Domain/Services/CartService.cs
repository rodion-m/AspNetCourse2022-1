using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace GreatShop.Domain.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartService> _logger;

        public CartService(
            ICartRepository cartRepository, 
            ILogger<CartService> logger)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task AddProduct(Guid accountId, Product product, double quantity = 1d)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            var cart = await _cartRepository.GetCartByAccountId(accountId);
            cart.Add(product, quantity);
            await _cartRepository.Update(cart);
        }

        public virtual Task<Cart> GetAccountCart(Guid accountId)
        {
            _logger.LogDebug("Get cart for account: {AccountId}", accountId);
            return _cartRepository.GetCartByAccountId(accountId);
        }
    }
}
