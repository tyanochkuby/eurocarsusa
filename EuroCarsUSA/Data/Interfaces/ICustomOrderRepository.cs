using EuroCarsUSA.Models.Form;

namespace EuroCarsUSA.Data.Interfaces
{
    public interface ICustomOrderRepository
    {
        Task<IEnumerable<CustomOrder>> GetAll();
        Task<CustomOrder> GetById(Guid id);
        Task<bool> Add(CustomOrder customOrder);
        Task<bool> Update(CustomOrder customOrder);
        Task<bool> Delete(CustomOrder customOrder);
        Task<bool> Save();
    }
}
