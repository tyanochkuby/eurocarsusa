using EuroCarsUSA.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Models
{
    public class Form
    {
        [Key]
        public Guid Id { get; set; }

        public User User { get; set; }
        public CarType CarType { get; set; }
        public CarMake Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int MaxPrice { get; set; }
        public int MaxMileage { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public CarFuelType CarFuelType { get; set; }
        public string Description { get; set; }

    }
}
