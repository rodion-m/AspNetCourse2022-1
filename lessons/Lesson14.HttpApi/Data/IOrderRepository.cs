using Lesson14.Models;

namespace Lesson14.HttpApi.Data;

public interface IOrderRepository
{
    Task<Order> GetById(Guid Id);
    Task<Order?> FindById(Guid Id);

    Task Add(Order order);
    Task Update(Order order);
    Task<IReadOnlyList<Order>> GetAll();
}