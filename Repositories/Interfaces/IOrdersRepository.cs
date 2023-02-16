using DailyAuto.Models;

namespace DailyAuto.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        List<Order> GetOrdersByUserId(long user_id, int limit, int offset);
        Order? GetOrderById(long id);
        Order CreateOrder(Order order);
    }
}
