using EuroCarsUSA.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models.Form
{
    public class FormCarTransmission
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FormId { get; set; }
        public Form Form { get; set; }

        public CarTransmission CarTransmission { get; set; }
    }
}
