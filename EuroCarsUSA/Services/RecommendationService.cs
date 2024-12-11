using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;

namespace EuroCarsUSA.Services;

public class RecommendationService : IRecommendationService
{
    private record CarScore(Car Car, int Score);

    private readonly ICarRepository _carRepository;

    private readonly ICookieService _cookieService;
    
    private const int RECENT_DAYS = 30;
    private const int RECENT_SCORE = 10;
    private const int PRICE_THRESHOLD = 5000;
    private const int VIEW_MAKE_MATCH_SCORE = 5;
    private const int VIEW_TYPE_MATCH_SCORE = 10;
    private const int VIEW_PRICE_MATCH_SCORE = 8;
    private const int LIKE_MAKE_MATCH_SCORE = 10;
    private const int LIKE_TYPE_MATCH_SCORE = 15;
    private const int LIKE_PRICE_MATCH_SCORE = 12;


    public RecommendationService(ICarRepository carRepository, ICookieService cookieService)
    {
        _carRepository = carRepository;
        _cookieService = cookieService;
    }

    public async Task<List<Car>> GetFirstNCars(int count)
    {
        var cars = (await _carRepository.GetAll()).ToList();
        var viewedIds = _cookieService.GetUserViewedCars();
        var likedIds = _cookieService.GetUserLikedCars();

        var likedCars = cars.Where(c => likedIds.Contains(c.Id)).ToList();
        var viewedCars = cars.Where(c => viewedIds.Contains(c.Id)).ToList();

        List<Car> topCars = cars.Select(x => new CarScore(x, CalculateCarScore(x, viewedCars, likedCars)))
                                       .OrderByDescending(c => c.Score)
                                       .Take(count)
                                       .Select(cs => cs.Car)
                                       .ToList();

        return topCars;
    }


    private int CalculateCarScore(Car car, List<Car> viewedCars, List<Car> likedCars)
    {
        int score = 0;


        // Match with user's viewed history
        foreach (var viewedCar in viewedCars)
        {
            if (car.Make == viewedCar.Make) score += VIEW_MAKE_MATCH_SCORE;
            if (car.Type == viewedCar.Type) score += VIEW_TYPE_MATCH_SCORE;
            if (Math.Abs(car.Price - viewedCar.Price) < PRICE_THRESHOLD) score += VIEW_PRICE_MATCH_SCORE;
        }

        // Match with user's liked history
        foreach (var likedCar in likedCars)
        {
            if (car.Make == likedCar.Make) score += LIKE_MAKE_MATCH_SCORE;
            if (car.Type == likedCar.Type) score += LIKE_TYPE_MATCH_SCORE;
            if (Math.Abs(car.Price - likedCar.Price) < PRICE_THRESHOLD) score += LIKE_PRICE_MATCH_SCORE;
        }

        // Recency boost
        if (DateTime.UtcNow.Subtract(car.TimeStamp).TotalDays < RECENT_DAYS)
        {
            score += RECENT_SCORE;
        }


        return score;
    }

    public async Task<Car?> GetLastAddedCar()
    {
        return (await _carRepository.GetAll()).OrderByDescending(c => c.TimeStamp).FirstOrDefault();
    }
}
