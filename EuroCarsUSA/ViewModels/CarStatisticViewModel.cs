using EuroCarsUSA.Data.Enum;

namespace EuroCarsUSA.ViewModels
{
    public class CarStatisticViewModel
    {
        public CarMake Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
    }
}
