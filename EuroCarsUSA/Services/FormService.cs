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
                Description = form.Description
            }).ToList();

            return viewModels;
        }

        public async Task<Guid?> SubmitFormAsync(FormViewModel formViewModel)
        {
            var form = new Form
            {
                Id = formViewModel.Id == Guid.Empty ? Guid.NewGuid() : formViewModel.Id,
                FormCarMakes = formViewModel.CarMakes.Select(cm => new FormCarMake { CarMake = cm }).ToList(),
                FormCarColors = formViewModel.CarColors.Select(cc => new FormCarColor { CarColor = cc }).ToList(),
                FormCarTypes = formViewModel.CarTypes.Select(ct => new FormCarType { CarType = ct }).ToList(),
                FormCarFuelTypes = formViewModel.CarFuelTypes.Select(cf => new FormCarFuelType { CarFuelType = cf }).ToList(),
                FormCarTransmissions = formViewModel.CarTransmissions.Select(ct => new FormCarTransmission { CarTransmission = ct }).ToList(),
                Model = formViewModel.Model,
                MaxPrice = formViewModel.MaxPrice,
                MaxMileage = formViewModel.MaxMileage,
                MinYear = formViewModel.MinYear,
                MaxYear = formViewModel.MaxYear,
                Description = formViewModel.Description
            };

            // Set the FormId and Form properties for related entities
            foreach (var carMake in form.FormCarMakes)
            {
                carMake.FormId = form.Id;
                carMake.Form = form;
            }

            foreach (var carColor in form.FormCarColors)
            {
                carColor.FormId = form.Id;
                carColor.Form = form;
            }

            foreach (var carType in form.FormCarTypes)
            {
                carType.FormId = form.Id;
                carType.Form = form;
            }

            foreach (var carFuelType in form.FormCarFuelTypes)
            {
                carFuelType.FormId = form.Id;
                carFuelType.Form = form;
            }

            foreach (var carTransmission in form.FormCarTransmissions)
            {
                carTransmission.FormId = form.Id;
                carTransmission.Form = form;
            }

            if (await _formRepository.Add(form))
            {
                return form.Id;
            }

            return null;
        }
    }
}
