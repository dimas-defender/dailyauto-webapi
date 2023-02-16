#nullable disable

namespace DailyAuto.ModelsDB
{
    public class UserDB
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string License { get; set; }

        public virtual ICollection<OrderDB> Orders { get; set; }
    }
}
