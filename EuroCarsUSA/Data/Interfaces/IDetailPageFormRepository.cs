using EuroCarsUSA.Models;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface IDetailPageFormRepository
    {
        Task<DetailPageForm> GetById(Guid id);
        Task<IEnumerable<DetailPageForm>> GetAll();
        Task<bool> Add(DetailPageForm detailPageForm);
        Task<bool> Update(DetailPageForm detailPageForm);
        Task<bool> Delete(int id);
        Task<bool> Save();
    }
}
