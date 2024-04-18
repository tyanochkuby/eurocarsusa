using EuroCarsUSA.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public string Image { get; set; }
        public CarFuelType CarFuelType { get; set; }

        public Car() { }

    }
}
