using DailyAuto.Models;

namespace DailyAuto.Services.Interfaces
{
    public interface ICarsService
    {
        List<Car> GetAvailableCars(int limit, int offset);
        Car? GetCarById(long id);
    }
}
