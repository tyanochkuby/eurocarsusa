using EuroCarsUSA.Data.Enum;
using EuroCarsUSA.Models;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task<IEnumerable<Car>> GetRange(int start, int count, CarFilter? filters);
        Task<Dictionary<string, List<string>>> GetAvailableFilters();
        Task<int> GetCount(CarFilter? filters);
        Task<Car> GetById(Guid id);
        Task<bool> Add(Car car);
        Task<bool> Update(Car car);
        Task<bool> Delete(Car car);
        Task<bool> Save();

    }
}
