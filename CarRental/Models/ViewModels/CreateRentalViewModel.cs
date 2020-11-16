using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models.ViewModels
{
    public class CreateRentalViewModel
    {
        public Rental Rental { get; set; }
        public List<Car> Cars { get; set; }
    }
}
