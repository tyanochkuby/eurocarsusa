using EuroCarsUSA.Data.Enums;

namespace EuroCarsUSA.Models
{
    public class CarFilter
    {
        
        public List<CarMake> Make { get; set; }
        public string Model { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? MinEngineVolume { get; set; }
        public int? MaxEngineVolume { get; set; }
        public int? MinMileage { get; set; }
        public int? MaxMileage { get; set; }
        public List<CarFuelType> FuelType { get; set; }
        public List<CarType> CarType { get; set; }
        public List<CarTransmission> Transmission { get; set; }
        public List<CarColor> Color { get; set; }
        public SortOrder SortOrder { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public CarStatus? Status { get; set; }

        public CarFilter() { }
    }
}
