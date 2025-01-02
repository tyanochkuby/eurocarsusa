using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using EuroCarsUSA.Services.Interfaces;

namespace EuroCarsUSA.Services
{
    public class CatalogEditingService : ICatalogEditingService
    {
        ICarRepository _carRepository;
        public CatalogEditingService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task UpdateRange(IEnumerable<Car> cars)
        {
            if (cars.Count() == 0) return;

            if (!await _carRepository.UpdateRange(cars))
            {
                throw new Exception("Failed to update cars");
            }
        }
        public async Task DeleteRange(IEnumerable<Guid> carIds)
        {
            if (carIds.Count() == 0) return;

            if (!await _carRepository.DeleteRange(carIds))
            {
                throw new Exception("Failed to delete cars");
            }
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _carRepository.GetAll(null);
        }

        public Task<IEnumerable<Car>> GetRange()
        {
            throw new NotImplementedException();
        }
    }
}
