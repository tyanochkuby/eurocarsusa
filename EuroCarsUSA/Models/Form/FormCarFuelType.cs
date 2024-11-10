using EuroCarsUSA.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models.Form
{

    public class FormCarFuelType
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FormId { get; set; }
        public Form Form { get; set; }

        public CarFuelType CarFuelType { get; set; }
    }

}
