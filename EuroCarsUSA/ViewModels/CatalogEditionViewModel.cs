using EuroCarsUSA.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.ViewModels
{
    public class CatalogEditionViewModel
    {
        public Guid Id { get; set; }
        public CarMake Make { get; set; }
        public CarType Type { get; set; }
        public string Model { get; set; }
        public CarColor Color { get; set; }
        public string VIN { get; set; }
        public List<string>? Images { get; set; }
        public string ImagesJson { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public CarFuelType FuelType { get; set; }
        public int? EngineVolume { get; set; }
        public CarTransmission Transmission { get; set; }
        public int Price { get; set; }

    }
}
