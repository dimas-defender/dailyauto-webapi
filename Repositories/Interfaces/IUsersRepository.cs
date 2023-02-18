using DailyAuto.Models;

namespace DailyAuto.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        User? GetUserById(long id);
        User? GetUserByLogin(string login);
        User CreateUser(User user);
        User UpdateUser(long id, User user);
    }
}
