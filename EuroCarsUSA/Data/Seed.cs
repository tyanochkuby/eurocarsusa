using EuroCarsUSA.Data.Enum;
using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

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
                            Make = CarMake.Tesla,
                            Type = CarType.Sedan,
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
                            Make = CarMake.MINI,
                            Type = CarType.Hatchback,
                            Model = "Countryman",
                            Color = "Blue",
                            VIN = "8765432",
                            Images = new List<string>{ "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDMkmd2hpZbC6_PeQRW9lViM3Aq17Bj2SU92A8FMOTyA&s" },
                            CarFuelType = Enum.CarFuelType.Benzine,
                            Year = 2020,
                            Mileage = 16734,
                        },
                        new Car()
                        {
                            Make = CarMake.Ford,
                            Type = CarType.Crossover,
                            Model = "Bronco Sport",
                            Color = "Grey",
                            VIN = "3196244231",
                            Images = new List<string>
                            {
                                "https://images.hgmsites.net/hug/2021-ford-bronco-sport-badlands_100791268_h.jpg",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8OQd2rLWdOWL_rKKagxIo7W-HrFbSgLYJXodnJ1funA&s",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSGsqp-TYlhLWpdJqtu7FYCTCr6PjY-smkp0Z54VKmxmg&s",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT97_3OVY0rbS8f7dd2Rk55NJQsEUVYGV92U0KobHsMVg&s"
                            },
                            CarFuelType = CarFuelType.Benzine,
                            Year = 2020,
                            Mileage = 23578,
                        },
                        new Car()
                        {
                            Make = CarMake.Ford,
                            Type = CarType.Sedan,
                            Model = "Mustang GT",
                            Color = "Black",
                            VIN = "147526830",
                            Images = new List<string>
                            {
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSb5H2UFTdzEUhh8TL-9qB45BXUAxWB-lmKABlL34H0wA&s",
                                "https://a57.foxnews.com/static.foxnews.com/foxnews.com/content/uploads/2022/07/896/500/black-accent.jpg?ve=1&tl=1",
                                "https://www.mustangspecs.com/wp-content/uploads/2022/01/maxresdefault.jpeg",
                                "https://www.ford.it/content/dam/guxeu/rhd/central/cars/2020-mustang-mach-1/launch/gallery/interior/Ford-Mach1-eu-FordMustangMach1_FighterJetGray_024-16x9-2160x1215-Gallery_D_T_M-2.jpg.renditions.original.png",
                            },
                            CarFuelType = CarFuelType.Benzine,
                            Year = 2021,
                            Mileage= 31493,
                        },
                        new Car()
                        {
                            Make = CarMake.Tesla,
                            Type = CarType.Sedan,
                            Model = "Model 3",
                            Color = "Black",
                            VIN = "234678134",
                            Images = new List<string>
                            {
                                "https://i.redd.it/bwmqt3c2eee11.jpg",
                                "https://i.pinimg.com/736x/af/01/45/af0145334577e505887598d4b0c97bba.jpg",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuKflDq0prQwRVctlgHpFwUtq48-cY6fllTZEEXiJRqA&s",
                                "https://i2.wp.com/americansupercars.com/wp-content/uploads/2020/11/20201028_173615-scaled.jpg?fit=2560%2C1920&ssl=1",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTAh2Jea1MVEYcqVO9TXu67tn_Rws5l8bbK349zIVznng&s"
                            },
                            CarFuelType = CarFuelType.Electric,
                            Year = 2022,
                            Mileage = 3242
                        },
                        new Car()
                        {
                            Make = CarMake.BMW,
                            Type = CarType.Sedan,
                            Model = "M5",
                            Color = "Blue",
                            VIN = "247856123",
                            Images = new List<string>
                            {
                                "https://file.kelleybluebookimages.com/kbb/base/house/2018/2018-BMW-M5-FrontSide_BMM51801_640x480.jpg",
                                "https://media.carsandbids.com/cdn-cgi/image/width=2080,quality=70/da4b9237bacccdf19c0760cab7aec4a8359010b0/photos/bVoBatLPE.4ch5lOvtM.jpg?t=161498178659",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxCWqrCLJtsxp54pssXxhoSw0SYQNiFtiH4R6BW6B9Pw&s",
                                "https://ireland.apollo.olxcdn.com/v1/files/eyJmbiI6IjF0NjhneHdiZ2d1czMtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.OTLlKoVeUIMbA5N_H985qFH5hRmxNPfE9F-Wjd54L70/image;s=0x450;q=70",
                                "https://ireland.apollo.olxcdn.com/v1/files/eyJmbiI6ImE4djg5eWFndzAxYy1PVE9NT1RPUEwiLCJ3IjpbeyJmbiI6IndnNGducXA2eTFmLU9UT01PVE9QTCIsInMiOiIxNiIsInAiOiIxMCwtMTAiLCJhIjoiMCJ9XX0.t6iEfyvXdY9zvIzAuEBRVWuX7OweMHByo2SUpxBN2zY/image;s=0x450;q=70"
                            },
                            CarFuelType = CarFuelType.Benzine,
                            Year = 2018,
                            Mileage = 68483
                        }
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
