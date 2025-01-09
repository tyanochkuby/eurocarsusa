using EuroCarsUSA.Models;

namespace EuroCarsUSA.ViewModels;

public class CatalogViewModel
{
    public List<CarCardViewModel> ShippingCars { get; set; }
    public List<CarCardViewModel> SoldCars { get; set; }
}
