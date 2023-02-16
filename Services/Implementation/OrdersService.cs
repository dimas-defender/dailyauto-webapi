using DailyAuto.Models;
using DailyAuto.Repositories.Interfaces;
using DailyAuto.Services.Interfaces;

namespace DailyAuto.Services.Implementation
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public List<Order> GetOrdersByUserId(long user_id, int limit, int offset)
        {
            return _ordersRepository.GetOrdersByUserId(user_id, limit, offset);
        }

        public Order? GetOrderById(long id)
        {
            return _ordersRepository.GetOrderById(id);
        }

        public Order CreateOrder(Order order)
        {
            return _ordersRepository.CreateOrder(order);
        }
    }
}
