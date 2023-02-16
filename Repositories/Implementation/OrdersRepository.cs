using DailyAuto.Database;
using DailyAuto.Models;
using DailyAuto.ModelsDB;
using DailyAuto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyAuto.Repositories.Implementation
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly Context _db;
        public OrdersRepository(Context db)
        {
            _db = db;
        }

        public static OrderDB BLtoDB(Order model)
        {
            return new OrderDB
            {
                Id = model.Id,
                UserId = model.UserId,
                CarId = model.CarId,
                TimeCreated = model.TimeCreated,
                DurationHours = model.DurationHours,
                Cost = model.Cost
            };
        }

        public static Order DBtoBL(OrderDB model)
        {
            return new Order
            (
                model.Id,
                model.UserId,
                model.CarId,
                model.TimeCreated,
                model.DurationHours,
                model.Cost
            );
        }

        public List<Order> GetOrdersByUserId(long user_id, int limit, int offset)
        {
            List<OrderDB> orders = _db.Orders.Where(f => f.UserId == user_id)
                                                .Skip(offset)
                                                .Take(limit)
                                                .ToList();

            List<Order> result = new List<Order>();
            foreach (var model in orders)
            {
                result.Add(DBtoBL(model));
            }
            return result;
        }

        public Order? GetOrderById(long id)
        {
            List<OrderDB> orders = _db.Orders.AsNoTracking().Where(f => f.Id == id).ToList();
            return orders.Count() > 0 ? DBtoBL(orders.First()) : null;
        }

        public Order CreateOrder(Order order)
        {
            OrderDB ord = BLtoDB(order);
            try
            {
                _db.Orders.Add(ord);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return DBtoBL(ord);
        }
    }
}
