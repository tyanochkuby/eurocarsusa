using EuroCarsUSA.Data.Enum;

namespace EuroCarsUSA.Models
{
    public class CarFilter
    {
        
        public CarMake? Make { get; set; }
        public string Model { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? MinEngineVolume { get; set; }
        public int? MaxEngineVolume { get; set; }
        public int? MinMileage { get; set; }
        public int? MaxMileage { get; set; }
        public CarFuelType? FuelType { get; set; }
        public CarType? CarType { get; set; }
        public CarTransmission? Transmission { get; set; }
        public string Color { get; set; }

            
        public CarFilter() { }
    }
}
