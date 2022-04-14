using Lesson14.Models;

namespace Lesson13.EFCore.Data;

public interface IOrderRepository
{
    Task<Order> GetById(Guid Id);
    Task<Order?> FindById(Guid Id);

    Task Add(Order order);
    Task Update(Order order);
}