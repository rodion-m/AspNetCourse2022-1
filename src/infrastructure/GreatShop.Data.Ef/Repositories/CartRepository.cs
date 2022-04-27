using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef.Repositories;

internal class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Cart> GetCartByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        var cart = await _dbContext.Carts
            .SingleOrDefaultAsync(it => it.AccountId == accountId, cancellationToken: cancellationToken)
            ?? _dbContext.Carts.Local.Single(it => it.AccountId == accountId)
            ;

        //await _dbContext.Entry(cart).Collection(it => it.Items).LoadAsync(cancellationToken);
        
        return cart;
    }

    public async Task<Cart?> FindCartByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Carts.FirstOrDefaultAsync(
            it => it.AccountId == accountId, cancellationToken: cancellationToken)
            ?? _dbContext.Carts.FirstOrDefault(it => it.AccountId == accountId
       );
    }
}