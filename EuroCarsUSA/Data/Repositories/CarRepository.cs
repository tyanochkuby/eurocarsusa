using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using EuroCarsUSA.Views.Home.Components.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;

namespace EuroCarsUSA.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        private Dictionary<SortOrderType, Func<IQueryable<Car>, IOrderedQueryable<Car>>> sortFunctions = new Dictionary<SortOrderType, Func<IQueryable<Car>, IOrderedQueryable<Car>>>
        {
            { SortOrderType.ByYear, cars => cars.OrderBy(c => c.Year) },
            { SortOrderType.ByYearDesc, cars => cars.OrderByDescending(c => c.Year) },
            { SortOrderType.ByMileage, cars => cars.OrderBy(c => c.Mileage) },
            { SortOrderType.ByPrice, cars => cars.OrderBy(c => c.Price) },
            { SortOrderType.ByPriceDesc, cars => cars.OrderByDescending(c => c.Price) },
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

        async Task<IEnumerable<Car>> ICarRepository.GetAll(CarStatus? status)
        {
            var repository = _context.Cars.AsQueryable();
            if (status.HasValue)
            {
                repository = repository.Where(c => c.Status == status.Value);
            }
            return await repository.ToListAsync();
        }

        async Task<Car> ICarRepository.GetById(Guid id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Car>> GetRange(int start, int count, CarFilter? filters, SortOrderType? sortOrder)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Start", start),
                new SqlParameter("@Count", count),
                new SqlParameter("@Make", filters?.Make != null && filters?.Make.Count > 0 ? string.Join(",", filters.Make.Select(m => (int)m)) : (object)DBNull.Value),
                new SqlParameter("@Model", filters?.Model ?? (object)DBNull.Value),
                new SqlParameter("@MinPrice", filters?.MinPrice ?? (object)DBNull.Value),
                new SqlParameter("@MaxPrice", filters?.MaxPrice ?? (object)DBNull.Value),
                new SqlParameter("@MinYear", filters?.MinYear ?? (object)DBNull.Value),
                new SqlParameter("@MaxYear", filters?.MaxYear ?? (object)DBNull.Value),
                new SqlParameter("@MinEngineVolume", filters?.MinEngineVolume ?? (object)DBNull.Value),
                new SqlParameter("@MaxEngineVolume", filters?.MaxEngineVolume ?? (object)DBNull.Value),
                new SqlParameter("@MinMileage", filters?.MinMileage ?? (object)DBNull.Value),
                new SqlParameter("@MaxMileage", filters?.MaxMileage ?? (object)DBNull.Value),
                new SqlParameter("@FuelType", filters?.FuelType != null && filters?.FuelType.Count > 0 ? string.Join(",", filters.FuelType.Select(f => (int)f)) : (object)DBNull.Value),
                new SqlParameter("@CarType", filters?.CarType != null && filters?.CarType.Count > 0 ? string.Join(",", filters.CarType.Select(t => (int)t)) : (object)DBNull.Value),
                new SqlParameter("@Transmission", filters?.Transmission != null && filters?.Transmission.Count > 0 ? string.Join(",", filters.Transmission.Select(t => (int)t)) : (object)DBNull.Value),
                new SqlParameter("@Color", filters?.Color != null && filters?.Color.Count > 0 ? string.Join(",", filters.Color.Select(c => (int)c)) : (object)DBNull.Value),
                new SqlParameter("@DateFrom", filters?.DateFrom ?? (object)DBNull.Value),
                new SqlParameter("@DateTo", filters?.DateTo ?? (object)DBNull.Value),
                new SqlParameter("@Status", filters?.Status != null && filters?.Status.Count > 0 ? string.Join(",", filters.Status.Select(m => (int)m)) : (object)DBNull.Value),
                new SqlParameter("@SortOrder", sortOrder ?? SortOrderType.NewFirst)
            };

            var cars = await _context.Cars.FromSqlRaw("EXEC GetFilteredCars @Start, @Count, @Make, @Model, @MinPrice, @MaxPrice, @MinYear, @MaxYear, @MinEngineVolume, @MaxEngineVolume, @MinMileage, @MaxMileage, @FuelType, @CarType, @Transmission, @Color, @DateFrom, @DateTo, @Status, @SortOrder", parameters.ToArray()).ToListAsync();

            return cars;
        }

        public async Task<int> GetCount(CarFilter? filters)
        {
            filters ??= new CarFilter(); // Initialize filters if null

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Make", filters.Make != null && filters.Make.Count > 0 ? string.Join(",", filters.Make.Select(m => (int)m)) : (object)DBNull.Value),
                new SqlParameter("@Model", !string.IsNullOrEmpty(filters.Model) ? filters.Model : (object)DBNull.Value),
                new SqlParameter("@MinPrice", filters.MinPrice.HasValue ? filters.MinPrice.Value : (object)DBNull.Value),
                new SqlParameter("@MaxPrice", filters.MaxPrice.HasValue ? filters.MaxPrice.Value : (object)DBNull.Value),
                new SqlParameter("@MinYear", filters.MinYear.HasValue ? filters.MinYear.Value : (object)DBNull.Value),
                new SqlParameter("@MaxYear", filters.MaxYear.HasValue ? filters.MaxYear.Value : (object)DBNull.Value),
                new SqlParameter("@MinEngineVolume", filters.MinEngineVolume.HasValue ? filters.MinEngineVolume.Value : (object)DBNull.Value),
                new SqlParameter("@MaxEngineVolume", filters.MaxEngineVolume.HasValue ? filters.MaxEngineVolume.Value : (object)DBNull.Value),
                new SqlParameter("@MinMileage", filters.MinMileage.HasValue ? filters.MinMileage.Value : (object)DBNull.Value),
                new SqlParameter("@MaxMileage", filters.MaxMileage.HasValue ? filters.MaxMileage.Value : (object)DBNull.Value),
                new SqlParameter("@FuelType", filters.FuelType != null && filters.FuelType.Count > 0 ? string.Join(",", filters.FuelType.Select(f => (int)f)) : (object)DBNull.Value),
                new SqlParameter("@CarType", filters.CarType != null && filters.CarType.Count > 0 ? string.Join(",", filters.CarType.Select(t => (int)t)) : (object)DBNull.Value),
                new SqlParameter("@Transmission", filters.Transmission != null && filters.Transmission.Count > 0 ? string.Join(",", filters.Transmission.Select(t => (int)t)) : (object)DBNull.Value),
                new SqlParameter("@Color", filters.Color != null && filters.Color.Count > 0 ? string.Join(",", filters.Color.Select(c => (int)c)) : (object)DBNull.Value),
                new SqlParameter("@DateFrom", filters.DateFrom.HasValue ? filters.DateFrom.Value : (object)DBNull.Value),
                new SqlParameter("@DateTo", filters.DateTo.HasValue ? filters.DateTo.Value : (object)DBNull.Value),
                new SqlParameter("@Status", filters?.Status != null && filters?.Status.Count > 0 ? string.Join(",", filters.Status.Select(m => (int)m)) : (object)DBNull.Value),
                new SqlParameter("@Count", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC GetFilteredCarsCount @Make, @Model, @MinPrice, @MaxPrice, @MinYear, @MaxYear, @MinEngineVolume, @MaxEngineVolume, @MinMileage, @MaxMileage, @FuelType, @CarType, @Transmission, @Color, @DateFrom, @DateTo, @Status, @Count OUTPUT", parameters.ToArray());

            return (int)parameters.Last().Value;
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

        public async Task<bool> UpdateRange(IEnumerable<Car> cars)
        {
            _context.Cars.UpdateRange(cars);
            return await Save();
        }

        public async Task<bool> DeleteRange(IEnumerable<Guid> carIds)
        {
            var carsToDelete = await _context.Cars.Where(c => carIds.Contains(c.Id)).ToListAsync();
            if (carsToDelete.Count == 0) return false;

            foreach (var car in carsToDelete)
            {
                // Delete images from the file system
                foreach (var image in car.Images)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", image);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }
            _context.Cars.RemoveRange(carsToDelete);
            return await Save();
        }

        public async Task<bool> AddImage(Guid carId, string imageUrl)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
            if (car == null)
                return false;

            car.Images.Add(imageUrl);
            return await Save();
        }
    }
}
