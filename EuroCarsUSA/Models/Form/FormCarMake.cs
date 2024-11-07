using System.ComponentModel.DataAnnotations;
using EuroCarsUSA.Data.Enum;

namespace EuroCarsUSA.Models.Form
{
    public class FormCarMake
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FormId { get; set; }
        public Form Form { get; set; }

        public CarMake CarMake { get; set; }

    }
}
