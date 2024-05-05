using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroCarsUSA.Models
{
    public class DetailPageForm
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(40)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        [MaxLength(9)]
        public string? PhoneNumber { get; set; }

        [ForeignKey("Car")]
        public Guid CarId { get; set; }
        public Car Car { get; set; }

        [MaxLength(200)]
        public string? Message { get; set; }

        public DetailPageForm() { }
    }
}
