using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef;

public class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Cart> GetCartByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        var cart = await _dbContext.Carts
            .SingleAsync(it => it.AccountId == accountId, cancellationToken: cancellationToken);

        await _dbContext.Entry(cart).Collection(it => it.Items).LoadAsync(cancellationToken);
        
        return cart;
    }

    public Task<Cart?> FindCartByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Carts.FirstOrDefaultAsync(
            it => it.AccountId == accountId, 
            cancellationToken: cancellationToken);
    }
}