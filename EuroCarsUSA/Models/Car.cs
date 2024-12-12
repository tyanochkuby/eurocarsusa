using EuroCarsUSA.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public CarMake Make { get; set; }
        public CarType Type { get; set; }
        public string Model { get; set; }
        public CarColor Color { get; set; }
        public string VIN { get; set; }
        public List<string> Images { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public CarFuelType FuelType { get; set; }
        public int? EngineVolume { get; set; }
        public CarTransmission Transmission { get; set; }
        public int Price { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public Car() { }

    }
}
