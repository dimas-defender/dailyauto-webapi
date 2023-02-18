using DailyAuto.Models;
using DailyAuto.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyAuto.Controllers
{
    [ApiController]
    [Route("api/v1/cars")]
    public class CarsController : ControllerBase
    {
        private ICarsService _carsService;
        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        /// <summary>
        /// Gets available cars
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Returns List(CarDTO)</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CarDTO>> GetAvailableCars(int limit, int offset)
        {
            if (limit < 0 || offset < 0)
                return BadRequest();

            var cars = _carsService.GetAvailableCars(limit, offset);

            if (cars.Count() == 0)
                return NoContent();

            List<CarDTO> result = new List<CarDTO>();
            foreach (var model in cars)
                result.Add(ModelToDTO(model));

            return Ok(result);
        }

        /// <summary>
        /// Gets car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns CarDTO</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarDTO> GetCarById(long id)
        {
            if (id < 1)
                return BadRequest();

            var model = _carsService.GetCarById(id);

            if (model is null)
                return NotFound();

            var dto = ModelToDTO(model);
            return Ok(dto);
        }
        private static CarDTO ModelToDTO(Car car) =>
            new CarDTO
            {
                Id = car.Id,
                Model = car.Model,
                IsAvailable = car.IsAvailable,
                Price = car.Price,
                Mileage = car.Mileage
            };
    }

    public class CarDTO
    {
        public long Id { get; set; }
        public string? Model { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; }
        public int Mileage { get; set; }
    }
}
