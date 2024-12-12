using EuroCarsUSA.Data.Enums;

namespace EuroCarsUSA.Data.DTOs
{
    public class CarStatisticDto
    {
        public Guid CarId { get; set; }
        public CarMake Make { get; set; }

        public CarType Type { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
    }
}
