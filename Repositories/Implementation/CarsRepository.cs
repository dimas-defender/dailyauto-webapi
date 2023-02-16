using DailyAuto.Database;
using DailyAuto.Models;
using DailyAuto.ModelsDB;
using DailyAuto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyAuto.Repositories.Implementation
{
    public class CarsRepository : ICarsRepository
    {
        private readonly Context _db;
        public CarsRepository(Context db)
        {
            _db = db;
        }

        public static CarDB BLtoDB(Car model)
        {
            return new CarDB
            {
                Id = model.Id,
                Model = model.Model,
                IsAvailable = model.IsAvailable,
                Price = model.Price,
                Mileage = model.Mileage
            };
        }

        public static Car DBtoBL(CarDB model)
        {
            return new Car
            (
                model.Id,
                model.Model,
                model.IsAvailable,
                model.Price,
                model.Mileage
            );
        }

        public List<Car> GetAvailableCars(int limit, int offset)
        {
            List<CarDB> cars = _db.Cars.Where(f => f.IsAvailable) //== true
                                    .Skip(offset)
                                    .Take(limit)
                                    .ToList();

            List<Car> result = new List<Car>();
            foreach (var model in cars)
            {
                result.Add(DBtoBL(model));
            }
            //return result.Count() > 0 ? result : null;
            return result;
        }

        public Car? GetCarById(long id)
        {
            List<CarDB> cars = _db.Cars.AsNoTracking().Where(f => f.Id == id).ToList();
            return cars.Count() > 0 ? DBtoBL(cars.First()) : null;
        }
    }
}
