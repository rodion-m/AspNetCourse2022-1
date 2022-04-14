using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;

namespace GreatShop.Infrastructure.Data;

public class CartItemRepository : EfRepository<CartItem>, ICartItemRepository
{
    public CartItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}