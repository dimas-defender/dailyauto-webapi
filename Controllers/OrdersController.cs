using DailyAuto.Models;
using DailyAuto.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DailyAuto.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrdersService _ordersService;
        private ICarsService _carsService;
        public OrdersController(IOrdersService ordersService, ICarsService carsService)
        {
            _ordersService = ordersService;
            _carsService = carsService;
        }

        /// <summary>
        /// Gets user's orders
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Returns List(OrderDTO)</returns>
        [Route("api/v1/users/{user_id}/orders")]
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<List<OrderDTO>> GetOrdersByUserId(long user_id, [FromQuery] int limit, [FromQuery] int offset)
        {
            if (user_id < 1 || limit < 0 || offset < 0)
                return BadRequest();

            var userId = long.Parse(User.Claims.First(i => i.Type == "user_id").Value);

            if (userId != user_id)
                return Forbid();

            var orders = _ordersService.GetOrdersByUserId(user_id, limit, offset);

            if (orders.Count() == 0)
                return NoContent();

            List<OrderDTO> result = new List<OrderDTO>();
            foreach (var model in orders)
                result.Add(ModelToDTO(model));

            return Ok(result);
        }

        /// <summary>
        /// Gets order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns OrderDTO</returns>
        [Route("api/v1/orders/{id}")]
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderDTO> GetOrderById(long id)
        {
            if (id < 1)
                return BadRequest();

            var model = _ordersService.GetOrderById(id);

            if (model is null)
                return NotFound();

            var user_id = long.Parse(User.Claims.First(i => i.Type == "user_id").Value);

            if (model.UserId != user_id)
                return Forbid();

            return Ok(ModelToDTO(model));
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="request">CreateOrderRequest</param>
        /// <returns>Returns OrderDTO</returns>
        [Route("api/v1/orders")]
        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<OrderDTO> CreateOrder(CreateOrderRequest request)
        {
            if (request.CarId < 1 || request.DurationHours < 1)
                return BadRequest();

            var user_id = long.Parse(User.Claims.First(i => i.Type == "user_id").Value);

            var car = _carsService.GetCarById(request.CarId);

            if (car is null || !car.IsAvailable)
                return BadRequest();

            var cost = car.Price * request.DurationHours;
            var order = new Order(0, user_id, request.CarId, DateTime.UtcNow, request.DurationHours, cost);

            var model = _ordersService.CreateOrder(order);
            var dto = ModelToDTO(model);

            return CreatedAtAction(nameof(GetOrderById), new { id = dto.Id }, dto);
        }

        private static OrderDTO ModelToDTO(Order order) =>
            new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                CarId = order.CarId,
                TimeCreated = order.TimeCreated,
                DurationHours = order.DurationHours,
                Cost = order.Cost
            };
    }
    public class CreateOrderRequest
    {
        [Required]
        public long CarId { get; set; }
        [Required]
        public int DurationHours { get; set; }
    }

    public class OrderDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CarId { get; set; }
        public DateTime TimeCreated { get; set; }
        public int DurationHours { get; set; }
        public int Cost { get; set; }
    }
}
