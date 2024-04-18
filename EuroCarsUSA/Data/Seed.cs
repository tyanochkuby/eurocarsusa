using EuroCarsUSA.Models;

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

                if (!context.Cars.Any())
                {
                    context.Cars.AddRange(new List<Car>()
                    {
                        new Car()
                        {
                            Make = "Tesla",
                            Model = "Model S P100D",
                            Color = "Red",
                            VIN = "12345364758",
                            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS2EqfTpmjeC6G23-aYZFOV55RfNpZ7rdRj7FQkzqCXjQ&s",
                            CarFuelType = Enum.CarFuelType.Electric
                        },
                        new Car()
                        {
                            Make = "Mini",
                            Model = "Countryman",
                            Color = "Blue",
                            VIN = "8765432",
                            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDMkmd2hpZbC6_PeQRW9lViM3Aq17Bj2SU92A8FMOTyA&s",
                            CarFuelType = Enum.CarFuelType.Benzine
                        }
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
