using EuroCarsUSA.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models.Form
{
    public class CustomOrder
    {
        [Key]
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<Form> Forms { get; set; } = new List<Form>();

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
