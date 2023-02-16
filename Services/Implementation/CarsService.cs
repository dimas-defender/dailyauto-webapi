using DailyAuto.Models;
using DailyAuto.Repositories.Interfaces;
using DailyAuto.Services.Interfaces;

namespace DailyAuto.Services.Implementation
{
    public class CarsService : ICarsService
    {
        private ICarsRepository _carsRepository;

        public CarsService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public List<Car> GetAvailableCars(int limit, int offset)
        {
            return _carsRepository.GetAvailableCars(limit, offset);
        }

        public Car? GetCarById(long id)
        {
            return _carsRepository.GetCarById(id);
        }
    }
}
