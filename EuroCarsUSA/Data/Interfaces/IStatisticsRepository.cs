using EuroCarsUSA.Data.DTOs;
using EuroCarsUSA.Models;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface IStatisticsRepository
    {
        public Task<List<CarStatisticDto>> GetCarsStatistics(int? pageNumber, int? pageSize);
        public Task ExecuteUpdateLikeCount(Guid carId, bool increment);
        public Task ExecuteUpdateViewsCount(Guid carId);
        Task<TotalStatsDto> GetTotalStats();
    }
}