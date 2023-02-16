namespace DailyAuto.Models
{
    public class Car
    {
        public Car(long id, string model, bool isavailable, int price, int mileage)
        {
            Id = id;
            Model = model;
            IsAvailable = isavailable;
            Price = price;
            Mileage = mileage;
        }

        public long Id { get; set; }
        public string Model { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; }
        public int Mileage { get; set; }
    }
}
