using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Models;
using EuroCarsUSA.Views.Home.Components.ViewModels;
using Microsoft.Extensions.Localization;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task<IEnumerable<Car>> GetRange(int start, int count, CarFilter? filters, SortOrder? sortOrder);
        Task<Dictionary<string, List<FilterOptionViewModel>>> GetAvailableFilters(IStringLocalizer localizer);
        Task<int> GetCount(CarFilter? filters);
        Task<Car> GetById(Guid id);
        Task<bool> Add(Car car);
        Task<bool> Update(Car car);
        Task<bool> UpdateRange(IEnumerable<Car> cars);
        Task<bool> Delete(Car car);
        Task<bool> DeleteRange(IEnumerable<Guid> carIds);
        Task<bool> Save();

        public Task<bool> AddImage(Guid carId, string imageUrl);

    }
}
