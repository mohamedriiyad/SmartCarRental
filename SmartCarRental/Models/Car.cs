using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarRental.Models
{
    public class Car
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string FirstLocation { get; set; }

        [Required]
        public string SecondLocation { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime AvailableFrom { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<UserRent> UserRents { get; set; }
        public ICollection<CarRent> CarRents { get; set; }
    }
}
