using EuroCarsUSA.Data.Enum;
using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Models.Form;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.ViewModels
{
    public class FormViewModel
    {
        public Guid Id { get; set; }
        public List<CarMake>? CarMakes { get; set; } = new List<CarMake>();
        public List<CarColor>? CarColors { get; set; } = new List<CarColor>();
        public List<CarType>? CarTypes { get; set; } = new List<CarType>();
        public List<CarFuelType>? CarFuelTypes { get; set; } = new List<CarFuelType>();
        public List<CarTransmission>? CarTransmissions { get; set; } = new List<CarTransmission>();
        public string? Model { get; set; }
        public int? MaxPrice { get; set; }
        public int? MaxMileage { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public string? Description { get; set; }

        public Form ToForm()
        {
            var formId = Id == Guid.Empty ? Guid.NewGuid() : Id;

            var form = new Form()
            {
                Id = formId,
                FormCarMakes = CarMakes.Select(cm => new FormCarMake { CarMake = cm, FormId = formId }).ToList(),
                FormCarColors = CarColors.Select(cc => new FormCarColor { CarColor = cc, FormId = formId }).ToList(),
                FormCarTypes = CarTypes.Select(ct => new FormCarType { CarType = ct, FormId = formId }).ToList(),
                FormCarFuelTypes = CarFuelTypes.Select(cf => new FormCarFuelType { CarFuelType = cf, FormId = formId }).ToList(),
                FormCarTransmissions = CarTransmissions.Select(ct => new FormCarTransmission { CarTransmission = ct, FormId = formId }).ToList(),
                Model = Model,
                MaxPrice = MaxPrice,
                MaxMileage = MaxMileage,
                MinYear = MinYear,
                MaxYear = MaxYear,
                Description = Description
            };
            return form;
        }
    }
}
