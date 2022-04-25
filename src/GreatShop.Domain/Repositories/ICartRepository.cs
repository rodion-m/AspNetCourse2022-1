using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetCartByAccountId(Guid accountId, CancellationToken cancellationToken = default);
    Task<Cart?> FindCartByAccountId(Guid accountId, CancellationToken cancellationToken = default);
}