using DailyAuto.Models;

namespace DailyAuto.Services.Interfaces
{
    public interface IUsersService
    {
        User? GetUserById(long id);
        User CreateUser(User user);
        User UpdateUser(long id, User user);
    }
}
