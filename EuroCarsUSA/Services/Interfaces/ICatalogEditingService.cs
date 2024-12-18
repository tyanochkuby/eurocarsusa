﻿using EuroCarsUSA.Models;

namespace EuroCarsUSA.Services.Interfaces
{
    public interface ICatalogEditingService
    {
        public Task UpdateRange(IEnumerable<Car> cars);
        public Task DeleteRange(IEnumerable<Guid> carIds);
        public Task<IEnumerable<Car>> GetAll();
        public Task<IEnumerable<Car>> GetRange();
    }
}
