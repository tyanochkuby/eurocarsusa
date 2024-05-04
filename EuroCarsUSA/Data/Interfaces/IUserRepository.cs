using EuroCarsUSA.Models;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByIdAsync(Guid id);
        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);
        Task<bool> Save();
    }
}
