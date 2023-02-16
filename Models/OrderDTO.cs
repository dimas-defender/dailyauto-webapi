namespace DailyAuto.Models
{
    public class OrderDTO
    {
        public OrderDTO(long carid, int durationhours, int cost)
        {
            CarId = carid;
            DurationHours = durationhours;
        }
        public long CarId { get; set; }
        public int DurationHours { get; set; }
    }
}
