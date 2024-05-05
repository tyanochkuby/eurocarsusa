using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroCarsUSA.Data.Repositories
{
    public class DetailPageFormRepository : IDetailPageFormRepository
    {
        private readonly AppDbContext _context;
        public DetailPageFormRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<bool> Add(DetailPageForm detailPageForm)
        {
            _context.Add(detailPageForm);
            return Save();
        }

        public Task<bool> Delete(int id)
        {
            _context.Remove(id);
            return Save();
        }

        public async Task<IEnumerable<DetailPageForm>> GetAll()
        {
            return await _context.DetailPageForms.ToListAsync();
        }

        public async Task<DetailPageForm> GetById(Guid id)
        {
            return await _context.DetailPageForms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public Task<bool> Update(DetailPageForm detailPageForm)
        {
            _context.Update(detailPageForm);
            return Save();
        }
    }
}
