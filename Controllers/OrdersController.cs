using DailyAuto.Models;
using DailyAuto.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyAuto.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        /// <summary>
        /// Gets user's orders
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [Route("api/v1/users/{user_id}/orders")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Order>> GetOrdersByUserId(long user_id, [FromQuery] int limit, [FromQuery] int offset)
        {
            var orders = _ordersService.GetOrdersByUserId(user_id, limit, offset);
            return Ok(orders);
        }

        [Route("api/v1/orders/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Order> GetOrderById(long id)
        {
            var model = _ordersService.GetOrderById(id);

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        [Route("api/v1/orders")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Order> CreateOrder(Order order)
        {
            var model = _ordersService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = model.Id }, model);
        }
    }
}
