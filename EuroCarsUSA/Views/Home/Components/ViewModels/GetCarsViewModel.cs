using EuroCarsUSA.Models;

namespace EuroCarsUSA.Views.Home.Components.ViewModels
{
    public class GetCarsViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public List<Guid> LikedCars { get; set; }
    
    }
}
