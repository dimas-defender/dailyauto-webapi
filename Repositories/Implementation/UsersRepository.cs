using DailyAuto.Database;
using DailyAuto.Models;
using DailyAuto.ModelsDB;
using DailyAuto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyAuto.Repositories.Implementation
{
    public class UsersRepository : IUsersRepository
    {
        private readonly Context _db;
        public UsersRepository(Context db)
        {
            _db = db;
        }
        public static UserDB BLtoDB(User model)
        {
            return new UserDB
            {
                Id = model.Id,
                Login = model.Login,
                Password = model.Password,
                License = model.License
            };
        }

        public static User DBtoBL(UserDB model)
        {
            return new User
            (
                model.Id,
                model.Login,
                model.Password,
                model.License
            );
        }

        public User? GetUserById(long id)
        {
            List<UserDB> users = _db.Users.AsNoTracking().Where(f => f.Id == id).ToList();
            return users.Count() > 0 ? DBtoBL(users.First()) : null;
        }

        public User? GetUserByLogin(string login)
        {
            List<UserDB> users = _db.Users.AsNoTracking().Where(f => f.Login == login).ToList();
            return users.Count() > 0 ? DBtoBL(users.First()) : null;
        }

        public User CreateUser(User user)
        {
            UserDB usr = BLtoDB(user);
            try
            {
                _db.Users.Add(usr);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return DBtoBL(usr);
        }

        public User UpdateUser(long id, User user)
        {
            UserDB usr = BLtoDB(user);
            try
            {
                _db.Users.Update(usr);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return DBtoBL(usr);
        }
    }
}
