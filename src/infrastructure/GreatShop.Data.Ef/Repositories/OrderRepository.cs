using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;

namespace GreatShop.Data.Ef.Repositories;

internal class OrderRepository : EfRepository<Order>, IOrderRepository
{

    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}