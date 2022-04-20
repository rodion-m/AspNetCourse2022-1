using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Infrastructure.Data;

public class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Cart> GetCartByAccountId(Guid accountId)
    {
        var cart = await _dbContext.Carts
            .SingleAsync(it => it.AccountId == accountId);

        await _dbContext.Entry(cart).Collection(it => it.Items).LoadAsync();
        
        return cart;
    }

    public Task<Cart?> FindCartByAccountId(Guid accountId)
    {
        return _dbContext.Carts.FirstOrDefaultAsync(it => it.AccountId == accountId);
    }
}