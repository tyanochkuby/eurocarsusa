using EuroCarsUSA.Models;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task<Car> GetByIdAsync(Guid id);
        Task<bool> Add(Car car);
        Task<bool> Update(Car car);
        Task<bool> Delete(Car car);
        Task<bool> Save();

    }
}
