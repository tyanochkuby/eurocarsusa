using EuroCarsUSA.Models;

namespace EuroCarsUSA.ViewModels
{
    public class DetailViewModel
    {
        public Car Car { get; set; }
        public DetailPageForm DetailPageForm { get; set; }
        public List<CarCardViewModel> RecomendedCar { get; set; }
    }
}
