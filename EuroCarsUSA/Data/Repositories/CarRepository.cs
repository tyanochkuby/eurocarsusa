using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Car>> GetRange(int start, int count)
        {
            return await _context.Cars.Skip(start).Take(count).ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Cars.CountAsync();
        }
    }
}
