using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarRental.Models
{
    public class CarRent
    {
        public string RenterId { get; set; }
        public int CarId { get; set; }
        public User Renter { get; set; }
        public Car Car { get; set; }
    }
}
