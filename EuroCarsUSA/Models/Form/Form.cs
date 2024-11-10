using EuroCarsUSA.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models.Form
{
    public class Form
    {
        [Key]
        public Guid Id { get; set; }
        public ICollection<FormCarMake>? FormCarMakes { get; set; } = new List<FormCarMake>();
        public ICollection<FormCarColor>? FormCarColors { get; set; } = new List<FormCarColor>();
        public ICollection<FormCarType>? FormCarTypes { get; set; } = new List<FormCarType>();
        public ICollection<FormCarFuelType>? FormCarFuelTypes { get; set; } = new List<FormCarFuelType>();
        public ICollection<FormCarTransmission>? FormCarTransmissions { get; set; } = new List<FormCarTransmission>();

        public string? Model { get; set; }
        public int? MaxPrice { get; set; }
        public int? MaxMileage { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public string? Description { get; set; }
    }
}
