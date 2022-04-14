using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Infrastructure.Data;

public class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Cart> GetCartByAccountId(Guid accountId)
    {
        return _dbContext.Carts.FirstAsync(it => it.AccountId == accountId);
    }

    public Task<Cart?> FindCartByAccountId(Guid accountId)
    {
        return _dbContext.Carts.FirstOrDefaultAsync(it => it.AccountId == accountId);
    }
}