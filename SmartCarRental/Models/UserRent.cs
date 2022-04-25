namespace SmartCarRental.Models
{
    public class UserRent
    {
        public string UserId { get; set; }
        public int CarId { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
    }
}
