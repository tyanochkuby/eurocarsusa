using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Models;

namespace EuroCarsUSA.ViewModels
{
    public class CarViewModel
    {
        static public CarViewModel FromCar(Car car, List<Guid> likedCars)
        {
            return new CarViewModel
            {
                Id = car.Id,
                Make = car.Make,
                Type = car.Type,
                Model = car.Model,
                Color = car.Color,
                VIN = car.VIN,
                Images = car.Images,
                Mileage = car.Mileage,
                Year = car.Year,
                FuelType = car.FuelType,
                EngineVolume = car.EngineVolume,
                Price = car.Price,
                Transmission = car.Transmission,
                IsLiked = likedCars.Contains(car.Id),
            };
        }
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
        public bool IsLiked { get; set; }

        public string Classes { get; set; }

        public bool IsFullSize { get; set; }
    }
}
