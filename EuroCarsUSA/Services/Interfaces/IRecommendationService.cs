using EuroCarsUSA.Models;

namespace EuroCarsUSA.Services.Interfaces
{
    public interface IRecommendationService
    {
        public Task<List<Car>> GetFirstNCars(int count);
        public Task<Car?> GetLastAddedCar();
    }
}
