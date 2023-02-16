namespace DailyAuto.Models
{
    public class User
    {
        public User(long id, string login, string password, string license)
        {
            Id = id;
            Login = login;
            Password = password;
            License = license;
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string License { get; set; }
    }
}
