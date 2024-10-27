using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroCarsUSA.Models
{
    public class CarStatistic
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Car")]
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
    }
}
