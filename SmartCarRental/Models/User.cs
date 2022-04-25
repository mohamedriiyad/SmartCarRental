using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarRental.Models
{
    public class User : IdentityUser
    {
        public ICollection<Car> Cars { get; set; }
        public ICollection<CarRent> CarRents { get; set; }
        public ICollection<UserRent> UserRents { get; set; }
    }
}
