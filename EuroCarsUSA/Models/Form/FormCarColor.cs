using EuroCarsUSA.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models.Form
{
    public class FormCarColor
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FormId { get; set; }
        public Form Form { get; set; }

        public CarColor CarColor { get; set; }
    }
}
