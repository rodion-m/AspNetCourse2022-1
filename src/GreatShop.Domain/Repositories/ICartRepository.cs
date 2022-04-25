using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetCartByAccountId(Guid accountId);
    Task<Cart?> FindCartByAccountId(Guid accountId);
}