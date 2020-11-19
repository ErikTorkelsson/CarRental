using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="varchar(250)")]
        [Required]
        public string Brand { get; set; }
        [Column(TypeName ="varchar(250)")]
        public string Model { get; set; }
        [Column(TypeName = "INT")]
        public int Year { get; set; }
        [Column(TypeName = "INT")]
        public int Mileage { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string Fuel { get; set; }
        [Column(TypeName = "INT")]
        public int Seats { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string combisudan { get; set; }
        [Column(TypeName ="varchar(500)")]
        public string about { get; set; }
        [Column(TypeName ="varchar(500)")]
        public string ImgSrc { get; set; }
    }
}
