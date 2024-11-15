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
        public FormStatus Status { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
