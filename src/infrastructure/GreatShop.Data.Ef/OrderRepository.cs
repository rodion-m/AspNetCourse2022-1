using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef;

internal class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Order> GetById(Guid id)
    {
        return _dbContext.Orders.FirstAsync(it => it.Id == id);
    }

    public Task<Order?> FindById(Guid id)
    {
        return _dbContext.Orders.FirstOrDefaultAsync(it => it.Id == id);
    }

    public async Task<IReadOnlyCollection<Order>> GetAll()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task Add(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Order order)
    {
        _dbContext.Entry(order).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public Task<Order?> GetOrderByCustomerPhone(string phone)
    {
        return _dbContext.Orders.FirstOrDefaultAsync(it => it.Phone == phone);
    }
}