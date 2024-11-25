using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.ViewModels;

namespace EuroCarsUSA.Services.Interfaces
{
    public interface ICustomOrderService
    {
        Task<Guid?> SubmitFormAsync(CustomOrderViewModel customOrderViewModel);
        Task<List<CustomOrderViewModel>> GetAll();
        Task<CustomOrderViewModel> GetById(Guid id);
        Task<bool> UpdateStatus(Guid id, OrderStatus status);
    }
}
