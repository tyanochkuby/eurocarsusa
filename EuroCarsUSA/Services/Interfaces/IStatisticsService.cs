using EuroCarsUSA.Models;
using EuroCarsUSA.ViewModels;

namespace EuroCarsUSA.Services.Interfaces
{
    public interface IStatisticsService
    {
        public Task<List<CarStatisticViewModel>> GetCarsStatistics(int? pageNumber, int? pageSize);
        public Task ViewCar(Guid carId);
        public Task LikeCar(Guid carId);
        public Task UnlikeCar(Guid carId);
        public Task<TotalStatsViewModel> GetTotalStats();
    }
}
