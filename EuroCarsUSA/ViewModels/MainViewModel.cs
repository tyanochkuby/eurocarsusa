using EuroCarsUSA.Models;

namespace EuroCarsUSA.ViewModels
{
    public class MainlViewModel
    {
        public List<CarCardViewModel> ShippingCars { get; set; }
        public List<CarCardViewModel> RecomendedCars { get; set; }
        public CarCardViewModel? LastAddedCar { get; set; }
        public List<CarCardViewModel> SoldCars { get; set; }
    }
}
