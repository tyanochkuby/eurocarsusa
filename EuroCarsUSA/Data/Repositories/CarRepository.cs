using EuroCarsUSA.Data.Enum;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using EuroCarsUSA.Views.Home.Components.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Drawing;

namespace EuroCarsUSA.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        private Dictionary<SortOrder, Func<IQueryable<Car>, IOrderedQueryable<Car>>> sortFunctions = new Dictionary<SortOrder, Func<IQueryable<Car>, IOrderedQueryable<Car>>>
        {
            { SortOrder.ByYear, cars => cars.OrderBy(c => c.Year) },
            { SortOrder.ByYearDesc, cars => cars.OrderByDescending(c => c.Year) },
            { SortOrder.ByMileage, cars => cars.OrderBy(c => c.Mileage) },
            { SortOrder.ByPrice, cars => cars.OrderBy(c => c.Price) },
            { SortOrder.ByPriceDesc, cars => cars.OrderByDescending(c => c.Price) },
        };
        public CarRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        async Task<bool> ICarRepository.Add(Car car)
        {
            _context.Add(car);
            return await Save();
        }

        async Task<bool> ICarRepository.Delete(Car car)
        {
            _context.Remove(car);
            return await Save();
        }
        async Task<bool> ICarRepository.Update(Car car)
        {
            _context.Update(car);
            return await Save();
        }

        async Task<IEnumerable<Car>> ICarRepository.GetAll()
        {
            return await _context.Cars.ToListAsync();
        }

        async Task<Car> ICarRepository.GetById(Guid id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Car>> GetRange(int start, int count, CarFilter? filters, SortOrder? sortOrder)
        {
            var cars = _context.Cars.AsQueryable();

            cars = ApplyFilters(cars, filters);
            sortOrder = sortOrder ?? SortOrder.NewFirst;
            if(sortFunctions.ContainsKey(sortOrder.Value))
            {
                cars = sortFunctions[sortOrder.Value](cars);
            }
            cars = cars.Skip(start).Take(count);
            var carsList = await cars.ToListAsync();
            return carsList;
        }

        public async Task<int> GetCount(CarFilter? filters)
        {
            var cars = _context.Cars.AsQueryable();

            cars = ApplyFilters(cars, filters);

            return await cars.CountAsync();
        }

        private IQueryable<Car> ApplyFilters(IQueryable<Car> cars, CarFilter? filters)
        {
            if (filters != null)
            {
                cars = _context.Cars.Where(c =>
                    (filters.Make.Count == 0 || filters.Make.Any(m => m == c.Make)) &&
                    (string.IsNullOrEmpty(filters.Model) || c.Model.Contains(filters.Model) || filters.Model.Contains(c.Model)) &&
                    (!filters.MinPrice.HasValue || c.Price >= filters.MinPrice.Value) &&
                    (!filters.MaxPrice.HasValue || c.Price <= filters.MaxPrice.Value) &&
                    (!filters.MinYear.HasValue || c.Year >= filters.MinYear.Value) &&
                    (!filters.MaxYear.HasValue || c.Year <= filters.MaxYear.Value) &&
                    (!filters.MinMileage.HasValue || c.Mileage >= filters.MinMileage.Value) && // Corrected here
                    (!filters.MaxMileage.HasValue || c.Mileage <= filters.MaxMileage.Value) && // Corrected here
                    (!filters.MinEngineVolume.HasValue || c.EngineVolume >= filters.MinEngineVolume.Value) &&
                    (!filters.MaxEngineVolume.HasValue || c.EngineVolume <= filters.MaxEngineVolume.Value) &&
                    (filters.FuelType.Count == 0 || filters.FuelType.Any(f => f == c.FuelType)) &&
                    (filters.CarType.Count == 0 || filters.CarType.Any(f => f == c.Type)) &&
                    (filters.Transmission.Count == 0 || filters.Transmission.Any(f => f == c.Transmission)) &&
                    (filters.Color.Count == 0 || filters.Color.Any(f => f == c.Color))
                );
            }
            return cars;
        }


        public async Task<Dictionary<string, List<FilterOptionViewModel>>> GetAvailableFilters(IStringLocalizer localizer)
        {
            var makes = await _context.Cars
                .Select(c => c.Make.ToString())
                .Distinct()
                .Select(make => new FilterOptionViewModel { OriginalValue = make, TranslatedValue = make }) 
                .ToListAsync();

            var colors = await _context.Cars
                .Select(c => c.Color)
                .Distinct()
                .Select(color => new FilterOptionViewModel
                {
                    OriginalValue = color.ToString(),
                    TranslatedValue = localizer[$"{typeof(CarColor).Name}_{color}"] 
                })
                .ToListAsync();

            var carTypes = await _context.Cars
                .Select(c => c.Type.ToString())
                .Distinct()
                .Select(carType => new FilterOptionViewModel
                {
                    OriginalValue = carType,
                    TranslatedValue = carType
                })
                .ToListAsync();

            var fuelTypes = await _context.Cars
                .Select(c => c.FuelType)
                .Distinct()
                .Select(fuelType => new FilterOptionViewModel
                {
                    OriginalValue = fuelType.ToString(),
                    TranslatedValue = localizer[$"{typeof(CarFuelType).Name}_{fuelType}"]
                })
                .ToListAsync();

            var transmissions = await _context.Cars
                .Select(c => c.Transmission)
                .Distinct()
                .Select(transmission => new FilterOptionViewModel
                {
                    OriginalValue = transmission.ToString(),
                    TranslatedValue = localizer[$"{typeof(CarTransmission).Name}_{transmission}"]
                })
                .ToListAsync();

            return new Dictionary<string, List<FilterOptionViewModel>>
            {
                { "make", makes },
                { "color", colors },
                { "carType", carTypes },
                { "fuelType", fuelTypes },
                { "transmission", transmissions }
            };
        }


    }
}
