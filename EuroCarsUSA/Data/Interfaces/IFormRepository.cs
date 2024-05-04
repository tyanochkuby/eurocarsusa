using EuroCarsUSA.Models;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface IFormRepository
    {
        Task<IEnumerable<Form>> GetAll();
        Task<Form> GetByIdAsync(Guid id);
        Task<bool> Add(Form form);
        Task<bool> Update(Form form);
        Task<bool> Delete(Form form);
        Task<bool> Save();
    }
}
