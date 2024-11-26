using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Models.Form;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.ViewModels
{
    public class CustomOrderViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<FormViewModel> Forms { get; set; } = new List<FormViewModel>();
        public OrderStatus Status { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
