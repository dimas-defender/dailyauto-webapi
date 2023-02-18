using DailyAuto.Models;
using DailyAuto.Repositories.Interfaces;
using DailyAuto.Services.Interfaces;

namespace DailyAuto.Services.Implementation
{
    public class UsersService : IUsersService
    {
        private IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User? GetUserById(long id)
        {
            return _usersRepository.GetUserById(id);
        }

        public User? GetUserByLogin(string login)
        {
            return _usersRepository.GetUserByLogin(login);
        }

        public User CreateUser(User user)
        {
            return _usersRepository.CreateUser(user);
        }

        public User UpdateUser(long id, User user)
        {
            return _usersRepository.UpdateUser(id, user);
        }
    }
}
