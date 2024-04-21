using EuroCarsUSA.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public List<string> Images { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public CarFuelType CarFuelType { get; set; }  //объем двигателя, коробка,

        public Car() { }

    }
}
