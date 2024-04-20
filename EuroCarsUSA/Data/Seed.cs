using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroCarsUSA.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (true)//!context.Cars.Any())
                {
                    context.Database.ExecuteSqlRaw("TRUNCATE TABLE Cars");
                    context.Cars.AddRange(new List<Car>()
                    {
                        new Car()
                        {
                            Make = "Tesla",
                            Model = "Model S P100D",
                            Color = "Red",
                            VIN = "12345364758",
                            Images = new List<string>{ 
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS2EqfTpmjeC6G23-aYZFOV55RfNpZ7rdRj7FQkzqCXjQ&s",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQGIRxwSRMqNNnahDvBfT3dsBfTC-nwAtph5DfNtXONaA&s",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS8noXZd9o7-nHxx4jZjG7NEcdbSt-Xn_8PxvKTSjr3yA&s",
                                "https://i.pinimg.com/736x/02/f9/eb/02f9ebc30503b86c7bbbc7c20915a5d2.jpg"
                            },
                            CarFuelType = Enum.CarFuelType.Electric,
                            Year=2018,
                            Mileage = 67890,
                        },
                        new Car()
                        {
                            Make = "Mini",
                            Model = "Countryman",
                            Color = "Blue",
                            VIN = "8765432",
                            Images = new List<string>{ "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDMkmd2hpZbC6_PeQRW9lViM3Aq17Bj2SU92A8FMOTyA&s" },
                            CarFuelType = Enum.CarFuelType.Benzine,
                            Year = 2020,
                            Mileage = 16734,
                        }
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
