using EuroCarsUSA.ViewModels;

namespace EuroCarsUSA.Services.Interfaces
{
    public interface IFormService
    {
        Task<Guid?> SubmitFormAsync(FormViewModel formViewModel);
        Task<List<FormViewModel>> GetAll();
    }
}
