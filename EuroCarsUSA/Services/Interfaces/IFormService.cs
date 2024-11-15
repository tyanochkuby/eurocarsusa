using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.ViewModels;

namespace EuroCarsUSA.Services.Interfaces
{
    public interface IFormService
    {
        Task<Guid?> SubmitFormAsync(FormViewModel formViewModel);
        Task<List<FormViewModel>> GetAll();
        Task<FormViewModel> GetById(Guid id);
        Task<bool> UpdateStatus(Guid id, FormStatus status);
    }
}
