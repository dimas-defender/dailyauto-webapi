#nullable disable

namespace DailyAuto.ModelsDB
{
    public class OrderDB
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CarId { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public int DurationHours { get; set; }
        public int Cost { get; set; }

        public virtual UserDB User { get; set; }
        public virtual CarDB Car { get; set; }
    }
}
