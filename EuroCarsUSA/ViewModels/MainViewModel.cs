using EuroCarsUSA.Models;

namespace EuroCarsUSA.ViewModels
{
    public class MainlViewModel
    {
        public List<CarCardViewModel> CarWDrodze { get; set; }
        public List<CarCardViewModel> RecomendedCar { get; set; }
        public CarCardViewModel LastAddedCar { get; set; }
    }
}
