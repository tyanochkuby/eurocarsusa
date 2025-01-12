using EuroCarsUSA.Data;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Data.Repositories;
using EuroCarsUSA.Models;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EuroCarsUSA.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ICookieService _cookiesService;
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(ICookieService cookiesService, IStatisticsRepository sqlProcedureRepository)
        {
            _cookiesService = cookiesService;
            _statisticsRepository = sqlProcedureRepository;
        }

        public async Task<List<CarStatisticViewModel>> GetCarsStatistics(int? pageNumber, int? pageSize)
        {
            var carStatistics = await _statisticsRepository.GetCarsStatistics(pageNumber, pageSize);

            return carStatistics.Select(stat => new CarStatisticViewModel
            {
                Make = stat.Make,
                Type = stat.Type,
                Model = stat.Model,
                Year = stat.Year,
                Likes = stat.Likes,
                Views = stat.Views,
            }).ToList();
        }

        public async Task LikeCar(Guid carId)
        {
            var likedCars = _cookiesService.GetUserLikedCars();

            if (likedCars.Contains(carId))
            {
                return;
            }

            likedCars.Add(carId);
            await _statisticsRepository.ExecuteUpdateLikeCount(carId, true);
            _cookiesService.UpdateUserLikedCars(likedCars);

        }
        public async Task UnlikeCar(Guid carId)
        {
            var likedCars = _cookiesService.GetUserLikedCars();

            if (!likedCars.Contains(carId))
            {
                return;
            }

            likedCars.Remove(carId);
            _cookiesService.UpdateUserLikedCars(likedCars);
            await _statisticsRepository.ExecuteUpdateLikeCount(carId, false);
        }

        public async Task ViewCar(Guid carId)
        {
            var viewedCars = _cookiesService.GetUserViewedCars();

            if (viewedCars.Contains(carId))
            {
                return;
            }

            viewedCars.Add(carId);
            await _statisticsRepository.ExecuteUpdateViewsCount(carId);
            _cookiesService.UpdateUserViewedCars(viewedCars);
        }

        public async Task<TotalStatsViewModel> GetTotalStats()
        {
            var totalStats = await _statisticsRepository.GetTotalStats();

            return new TotalStatsViewModel
            {
                TotalLikes = totalStats.TotalLikes,
                TotalViews = totalStats.TotalViews,
                TotalCustomOrders = totalStats.TotalCustomOrders,
                TotalDetailPageForms = totalStats.TotalDetailPageForms,
                LikesLastMonth = totalStats.LikesLastMonth,
                ViewsLastMonth = totalStats.ViewsLastMonth
            };
        }
    }
}
