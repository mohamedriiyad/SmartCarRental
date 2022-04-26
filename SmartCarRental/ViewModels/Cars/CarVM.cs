using System;

namespace SmartCarRental.ViewModels.Cars
{
    public class CarVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string FirstLocation { get; set; }
        public string SecondLocation { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime AvailableFrom { get; set; }
    }
}