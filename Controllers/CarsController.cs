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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Car>> GetAvailableCars(int limit, int offset)
        {
            var cars = _carsService.GetAvailableCars(limit, offset);
            return Ok(cars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Car> GetCarById(long id)
        {
            var model = _carsService.GetCarById(id);

            if (model is null)
                return NotFound();

            return Ok(model);
        }
    }
}
