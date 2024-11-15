using EuroCarsUSA.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.ViewModels
{
    public class CatalogEditionViewModel
    {
        public Guid Id { get; set; } = new Guid();
        public CarMake Make { get; set; }
        public CarType Type { get; set; }

        [Required]
        public string Model { get; set; }
        public CarColor Color { get; set; }
        public string VIN { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public string ImagesJson { get; set; } = "[]";

        [Required]
        [Range(0, 1_000_000)]
        public int Mileage { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }
        public CarFuelType FuelType { get; set; }
        public int? EngineVolume { get; set; }
        public CarTransmission Transmission { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }

    }
}
