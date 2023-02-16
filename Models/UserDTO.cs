namespace DailyAuto.Models
{
    public class UserDTO
    {
        public UserDTO(string login, string password, string license)
        {
            Login = login;
            Password = password;
            License = license;
        }
        public string Login { get; set; }
        public string Password { get; set; }
        public string License { get; set; }
    }
}