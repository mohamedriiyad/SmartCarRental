using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCarRental.ViewModels.Cars
{
    public class CarSearchVM
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
    }
}
