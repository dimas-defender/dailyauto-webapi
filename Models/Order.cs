namespace DailyAuto.Models
{
    public class Order
    {
        public Order(long id, long userid, long carid, DateTime timecreated, int durationhours, int cost)
        {
            Id = id;
            UserId = userid;
            CarId = carid;
            TimeCreated = timecreated;
            DurationHours = durationhours;
            Cost = cost;
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public long CarId { get; set; }
        public DateTime TimeCreated { get; set; }
        public int DurationHours { get; set; }
        public int Cost { get; set; }
    }
}
