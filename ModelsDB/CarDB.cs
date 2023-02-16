#nullable disable

namespace DailyAuto.ModelsDB
{
    public class CarDB
    {
        public long Id { get; set; }
        public string Model { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int Price { get; set; }
        public int Mileage { get; set; }
        public virtual ICollection<OrderDB> Orders { get; set; }
    }
}
