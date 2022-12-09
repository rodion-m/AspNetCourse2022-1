using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef.Repositories;

internal class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Cart> GetCartByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        var cart = await Entities
            .SingleOrDefaultAsync(it => it.AccountId == accountId, cancellationToken: cancellationToken)
            ?? Entities.Local.Single(it => it.AccountId == accountId)
            ;

        //await _dbContext.Entry(cart).Collection(it => it.Items).LoadAsync(cancellationToken);
        
        return cart;
    }

    public async Task<Cart?> FindCartByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        return await Entities.FirstOrDefaultAsync(
            it => it.AccountId == accountId, cancellationToken: cancellationToken)
            ?? Entities.Local.FirstOrDefault(it => it.AccountId == accountId);
    }
}