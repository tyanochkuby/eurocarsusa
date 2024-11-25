using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Models.Form;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.ViewModels
{
    public class CustomOrderViewModel
    {
        public Guid Id { get; set; }
        IEnumerable<Form> Forms { get; set; } = new List<Form>();
        public OrderStatus Status { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
