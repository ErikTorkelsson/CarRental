using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public string Rentee { get; set; }
        [Required]
        public int CarId { get; set; }
        public string Car { get; set; }

        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
    }
}
