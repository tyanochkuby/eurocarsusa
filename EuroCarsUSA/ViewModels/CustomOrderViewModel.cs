using EuroCarsUSA.Data.Attributes;
using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Models.Form;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EuroCarsUSA.ViewModels
{
    public class CustomOrderViewModel
    {
        public CustomOrderViewModel()
        {
            Forms.Add(new FormViewModel());
        }
        public Guid Id { get; set; }
        public List<FormViewModel> Forms { get; set; } = new List<FormViewModel>();
        public OrderStatus Status { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        [AtLeastOneProperty("Email", "PhoneNumber", ErrorMessage = "You must provide either an Email or a Phone Number.")]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Timestamp]
        public DateTime TimeStamp { get; set; }
    }
}
