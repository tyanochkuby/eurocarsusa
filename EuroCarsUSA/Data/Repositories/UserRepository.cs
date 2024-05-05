using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroCarsUSA.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        async Task<bool> IUserRepository.Add(User user)
        {
            _context.Add(user);
            return await Save();
        }

        async Task<bool> IUserRepository.Delete(User user)
        {
            _context.Remove(user);
            return await Save();
        }
        async Task<bool> IUserRepository.Update(User user)
        {
            _context.Update(user);
            return await Save();
        }

        async Task<IEnumerable<User>> IUserRepository.GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        async Task<User> IUserRepository.GetById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
        }




    }
}
