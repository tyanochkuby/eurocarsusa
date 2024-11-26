using EuroCarsUSA.Data;
using EuroCarsUSA.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using EuroCarsUSA.Models.Form;

namespace EuroCarsUSA.Data.Repositories
{
    public class CustomOrderRepository : ICustomOrderRepository
    {
        private readonly AppDbContext _context;
        public CustomOrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        async Task<bool> ICustomOrderRepository.Add(CustomOrder customorder)
        {
            _context.Add(customorder);
            return await Save();
        }

        async Task<bool> ICustomOrderRepository.Delete(CustomOrder customorder)
        {
            _context.Remove(customorder);
            return await Save();
        }
        async Task<bool> ICustomOrderRepository.Update(CustomOrder customorder)
        {
            _context.Update(customorder);
            return await Save();
        }

        async Task<IEnumerable<CustomOrder>> ICustomOrderRepository.GetAll()
        {
            return await _context.CustomOrders
            .Include(co => co.Forms)
                .ThenInclude(f => f.FormCarMakes)
            .Include(co => co.Forms)
                .ThenInclude(f => f.FormCarColors)
            .Include(co => co.Forms)
                .ThenInclude(f => f.FormCarTypes)
            .Include(co => co.Forms)
                .ThenInclude(f => f.FormCarFuelTypes)
            .Include(co => co.Forms)
                .ThenInclude(f => f.FormCarTransmissions)
            .AsSplitQuery()
            .ToListAsync();
        }

        async Task<CustomOrder?> ICustomOrderRepository.GetById(Guid id)
        {
            return await _context.CustomOrders
                .Include(co => co.Forms)
                    .ThenInclude(f => f.FormCarMakes)
                .Include(co => co.Forms)
                    .ThenInclude(f => f.FormCarColors)
                .Include(co => co.Forms)
                    .ThenInclude(f => f.FormCarTypes)
                .Include(co => co.Forms)
                    .ThenInclude(f => f.FormCarFuelTypes)
                .Include(co => co.Forms)
                    .ThenInclude(f => f.FormCarTransmissions)
                .AsSplitQuery()
                .FirstOrDefaultAsync(co => co.Id == id);
        }
    }
}
