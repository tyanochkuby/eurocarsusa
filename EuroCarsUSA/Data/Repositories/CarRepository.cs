using EuroCarsUSA.Data.Enum;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace EuroCarsUSA.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
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

        public async Task<IEnumerable<Car>> GetRange(int start, int count, CarFilter? filters)
        {
            var cars = _context.Cars.AsQueryable();

            cars = ApplyFilters(cars, filters);

            return await cars.Skip(start).Take(count).ToListAsync();
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
                    (!filters.MinMileage.HasValue || c.Year >= filters.MinMileage.Value) &&
                    (!filters.MaxMileage.HasValue || c.Year <= filters.MaxMileage.Value) &&
                    (!filters.MinEngineVolume.HasValue || c.EngineVolume >= filters.MinEngineVolume.Value) &&
                    (!filters.MaxEngineVolume.HasValue || c.EngineVolume <= filters.MaxEngineVolume.Value) &&
                    (!filters.FuelType.HasValue || c.FuelType == filters.FuelType.Value) &&
                    (!filters.CarType.HasValue || c.Type == filters.CarType.Value) &&
                    (!filters.Transmission.HasValue || c.Transmission == filters.Transmission.Value) &&
                    (string.IsNullOrEmpty(filters.Color) || c.Color == filters.Color)
                );
            }
            return cars;
        }

        public async Task<IEnumerable<CarMake>> GetMakes()
        {
            var makes = await _context.Cars.Select(c => c.Make).Distinct().ToListAsync();
            return makes;
        }
    }
}
