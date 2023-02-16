using DailyAuto.Models;

namespace DailyAuto.Repositories.Interfaces
{
    public interface ICarsRepository
    {
        List<Car> GetAvailableCars(int limit, int offset);
        Car? GetCarById(long id);
    }
}
