using EuroCarsUSA.Data.Attributes;
using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Models.Form;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<CustomOrderViewModel> Sent { get; set; }
        public IEnumerable<CustomOrderViewModel> Opened { get; set; }
        public IEnumerable<CustomOrderViewModel> Closed { get; set; }
    }
}
