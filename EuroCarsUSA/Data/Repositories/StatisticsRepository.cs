using EuroCarsUSA.Data.DTOs;
using EuroCarsUSA.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EuroCarsUSA.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AppDbContext _context;

        public StatisticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteUpdateLikeCount(Guid carId, bool increment)
        {
            var incrementParam = new SqlParameter("@Increment", increment ? 1 : 0);
            var carIdParam = new SqlParameter("@CarId", carId);

            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateLikesCount @CarId, @Increment", carIdParam, incrementParam);
        }

        public async Task ExecuteUpdateViewsCount(Guid carId)
        {
            var carIdParam = new SqlParameter("@CarId", carId);

            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateViewsCount @CarId", carIdParam);
        }
        public async Task<List<CarStatisticDto>> GetCarsStatistics(int? pageNumber, int? pageSize)
        {
            var query = _context.CarStatistics
                .Join(
                    _context.Cars,
                    stat => stat.CarId,
                    car => car.Id,
                    (stat, car) => new CarStatisticDto
                    {
                        CarId = stat.CarId,
                        Make = car.Make,
                        Type = car.Type,
                        Model = car.Model,
                        Year = car.Year,
                        Likes = stat.Likes,
                        Views = stat.Views,
                    }
                );

            if(pageNumber.HasValue && pageSize.HasValue)
            {
                query = query
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
    }
}
