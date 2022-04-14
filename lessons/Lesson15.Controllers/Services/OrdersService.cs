using Lesson15.Controllers.Models;

namespace Lesson15.Controllers.Services;

public class OrdersService
{
    private readonly IRepository<Order> _orderRepository;

    public OrdersService(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }
}