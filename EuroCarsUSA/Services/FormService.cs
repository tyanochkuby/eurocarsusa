using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models.Form;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;

namespace EuroCarsUSA.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;

        public FormService(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<List<FormViewModel>> GetAll()
        {
            var forms = await _formRepository.GetAll();
            
            var viewModels = forms.Select(form => new FormViewModel()
            {
                Id = form.Id,
                CarMakes = form.FormCarMakes.Select(cm => cm.CarMake).ToList(),
                CarColors = form.FormCarColors.Select(cc => cc.CarColor).ToList(),
                CarTypes = form.FormCarTypes.Select(ct => ct.CarType).ToList(),
                CarFuelTypes = form.FormCarFuelTypes.Select(cf => cf.CarFuelType).ToList(),
                CarTransmissions = form.FormCarTransmissions.Select(ct => ct.CarTransmission).ToList(),
                Model = form.Model,
                MaxPrice = form.MaxPrice,
                MaxMileage = form.MaxMileage,
                MinYear = form.MinYear,
                MaxYear = form.MaxYear,
                Description = form.Description,
                Status = form.Status
            }).ToList();

            return viewModels;
        }

        public async Task<FormViewModel> GetById(Guid id)
        {
            var form = await _formRepository.GetById(id);
            if (form == null)
            {
                return null;
            }

            var viewModel = new FormViewModel
            {
                Id = form.Id,
                CarMakes = form.FormCarMakes.Select(cm => cm.CarMake).ToList(),
                CarColors = form.FormCarColors.Select(cc => cc.CarColor).ToList(),
                CarTypes = form.FormCarTypes.Select(ct => ct.CarType).ToList(),
                CarFuelTypes = form.FormCarFuelTypes.Select(cf => cf.CarFuelType).ToList(),
                CarTransmissions = form.FormCarTransmissions.Select(ct => ct.CarTransmission).ToList(),
                Model = form.Model,
                MaxPrice = form.MaxPrice,
                MaxMileage = form.MaxMileage,
                MinYear = form.MinYear,
                MaxYear = form.MaxYear,
                Description = form.Description,
                Status = form.Status
            };

            return viewModel;
        }

        public async Task<Guid?> SubmitFormAsync(FormViewModel formViewModel)
        {
            var formId = formViewModel.Id == Guid.Empty ? Guid.NewGuid() : formViewModel.Id;

            var form = new Form
            {
                Id = formId,
                FormCarMakes = formViewModel.CarMakes.Select(cm => new FormCarMake { CarMake = cm, FormId = formId }).ToList(),
                FormCarColors = formViewModel.CarColors.Select(cc => new FormCarColor { CarColor = cc, FormId = formId }).ToList(),
                FormCarTypes = formViewModel.CarTypes.Select(ct => new FormCarType { CarType = ct, FormId = formId }).ToList(),
                FormCarFuelTypes = formViewModel.CarFuelTypes.Select(cf => new FormCarFuelType { CarFuelType = cf, FormId = formId }).ToList(),
                FormCarTransmissions = formViewModel.CarTransmissions.Select(ct => new FormCarTransmission { CarTransmission = ct, FormId = formId }).ToList(),
                Model = formViewModel.Model,
                MaxPrice = formViewModel.MaxPrice,
                MaxMileage = formViewModel.MaxMileage,
                MinYear = formViewModel.MinYear,
                MaxYear = formViewModel.MaxYear,
                Description = formViewModel.Description,
                Status = Data.Enums.FormStatus.Sent,
                Email = formViewModel.Email,
                PhoneNumber = formViewModel.PhoneNumber,
                Name = formViewModel.Name
            };

            if (await _formRepository.Add(form))
            {
                return form.Id;
            }

            return null;
        }

        public async Task<bool> UpdateStatus(Guid id, FormStatus status)
        {
            var form = await _formRepository.GetById(id);
            if (form == null)
            {
                return false;
            }
            form.Status = status;
            return await _formRepository.Update(form);
        }
    }
}
