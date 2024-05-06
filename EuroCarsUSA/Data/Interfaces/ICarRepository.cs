using EuroCarsUSA.Models;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task<IEnumerable<Car>> GetRange(int start, int count);
        Task<int> GetCount();
        Task<Car> GetById(Guid id);
        Task<bool> Add(Car car);
        Task<bool> Update(Car car);
        Task<bool> Delete(Car car);
        Task<bool> Save();

    }
}
