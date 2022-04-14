using Lesson14.HttpApi.Data;
using Lesson14.Models;

namespace Lesson14.HttpApi.Services;

public class OrdersService
{
    private readonly IOrderRepository _orderRepository;

    public OrdersService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task OfferOrder(Order order)
    {
        ValidateOrder(order);
        order.Status = OrderStatus.Offered;
        await _orderRepository.Update(order);
    }

    private void ValidateOrder(Order order)
    {
    }
}