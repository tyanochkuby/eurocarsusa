using EuroCarsUSA.Data;
using EuroCarsUSA.Models;
using EuroCarsUSA.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EuroCarsUSA.Data.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly AppDbContext _context;
        public FormRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        async Task<bool> IFormRepository.Add(Form form)
        {
            _context.Add(form);
            return await Save();
        }

        async Task<bool> IFormRepository.Delete(Form form)
        {
            _context.Remove(form);
            return await Save();
        }
        async Task<bool> IFormRepository.Update(Form form)
        {
            _context.Update(form);
            return await Save();
        }

        async Task<IEnumerable<Form>> IFormRepository.GetAll()
        {
            return await _context.Forms.ToListAsync();
        }

        async Task<Form> IFormRepository.GetByIdAsync(Guid id)
        {
            return await _context.Forms.FirstOrDefaultAsync(c => c.Id == id);
        }




    }
}
