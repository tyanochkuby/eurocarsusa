using EuroCarsUSA.Models.Form;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface IFormRepository
    {
        Task<IEnumerable<Form>> GetAll();
        Task<Form> GetById(Guid id);
        Task<bool> Add(Form form);
        Task<bool> Update(Form form);
        Task<bool> Delete(Form form);
        Task<bool> Save();
    }
}
