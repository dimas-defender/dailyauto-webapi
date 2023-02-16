using DailyAuto.Models;

namespace DailyAuto.Services.Interfaces
{
    public interface IOrdersService
    {
        List<Order> GetOrdersByUserId(long user_id, int limit, int offset);
        Order? GetOrderById(long id);
        Order CreateOrder(Order order);
    }
}
