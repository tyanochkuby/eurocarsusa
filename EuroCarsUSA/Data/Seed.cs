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
                    //context.Database.ExecuteSqlRaw("TRUNCATE TABLE Cars");
                    context.Cars.AddRange(new List<Car>()
                    {
                        new Car()
                        {
                            Make = CarMake.Tesla,
                            Type = CarType.Sedan,
                            Model = "Model S P100D",
                            Color = CarColor.Red,
                            VIN = "12345364758",
                            Images = new List<string> {
                                "https://ireland.apollo.olxcdn.com/v1/files/cztpfk9k5fly2-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/d7rpbu4v9xsk-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/e0gvt3e84zp53-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/7nbsrjhbrs1u1-PL/image;s=1000x700",
                            },
                            FuelType = Enum.CarFuelType.Electric,
                            Year = 2018,
                            Mileage = 67890,
                            Price = 350_000,
                            Transmission = CarTransmission.Automatic,
                        },
                        new Car()
                        {
                            Make = CarMake.MINI,
                            Type = CarType.Hatchback,
                            Model = "Countryman",
                            Color = CarColor.Blue,
                            VIN = "8765432",
                            Images = new List<string>
                            { 
                                "https://ireland.apollo.olxcdn.com/v1/files/utz5f9234x912-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/as9w7zi464pi1-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/4lkywrz7kpx53-PL/image;s=1000x700"

                            },
                            FuelType = Enum.CarFuelType.Benzine,
                            Year = 2020,
                            Mileage = 16734,
                            EngineVolume = 2800,
                            Transmission = CarTransmission.Automatic,
                            Price = 235_000,
                        },
                        new Car()
                        {
                            Make = CarMake.Ford,
                            Type = CarType.Crossover,
                            Model = "Bronco Sport",
                            Color = CarColor.Gray,
                            VIN = "3196244231",
                            Images = new List<string>
                            {
                                "https://images.hgmsites.net/hug/2021-ford-bronco-sport-badlands_100791268_h.jpg",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8OQd2rLWdOWL_rKKagxIo7W-HrFbSgLYJXodnJ1funA&s",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSGsqp-TYlhLWpdJqtu7FYCTCr6PjY-smkp0Z54VKmxmg&s",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT97_3OVY0rbS8f7dd2Rk55NJQsEUVYGV92U0KobHsMVg&s"
                            },
                            FuelType = CarFuelType.Benzine,
                            Year = 2020,
                            Mileage = 23578,
                            EngineVolume = 3900,
                            Transmission = CarTransmission.Automatic,
                            Price = 370_000
                        },
                        new Car()
                        {
                            Make = CarMake.Ford,
                            Type = CarType.Sedan,
                            Model = "Mustang GT",
                            Color = CarColor.Black,
                            VIN = "147526830",
                            Images = new List<string>
                            {
                                "https://ireland.apollo.olxcdn.com/v1/files/xvni7hytqkg52-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/me75bbca0o4s1-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/jfwak44z982u3-PL/image;s=1000x700",
                            },
                            FuelType = CarFuelType.Benzine,
                            Year = 2021,
                            Mileage = 31493,
                            EngineVolume = 3500,
                            Transmission = CarTransmission.Manual,
                            Price = 220_000,
                        },
                        new Car()
                        {
                            Make = CarMake.Tesla,
                            Type = CarType.Sedan,
                            Model = "Model 3",
                            Color = CarColor.Black,
                            VIN = "234678134",
                            Images = new List<string>
                            {
                                "https://i.redd.it/bwmqt3c2eee11.jpg",
                                "https://i.pinimg.com/736x/af/01/45/af0145334577e505887598d4b0c97bba.jpg",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuKflDq0prQwRVctlgHpFwUtq48-cY6fllTZEEXiJRqA&s",
                                "https://i2.wp.com/americansupercars.com/wp-content/uploads/2020/11/20201028_173615-scaled.jpg?fit=2560%2C1920&ssl=1",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTAh2Jea1MVEYcqVO9TXu67tn_Rws5l8bbK349zIVznng&s"
                            },
                            FuelType = CarFuelType.Electric,
                            Year = 2022,
                            Mileage = 3242,
                            Price = 200_000,
                            Transmission = CarTransmission.Automatic,
                        },
                        new Car()
                        {
                            Make = CarMake.BMW,
                            Type = CarType.Sedan,
                            Model = "M5",
                            Color = CarColor.Blue,
                            VIN = "247856123",
                            Images = new List<string>
                            {
                                "https://ireland.apollo.olxcdn.com/v1/files/j6mky1s62dqw1-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/24hdvd64sb702-PL/image;s=1000x700",
                                "https://ireland.apollo.olxcdn.com/v1/files/wo16f6bd0076-PL/image;s=1000x700",
                            },
                            FuelType = CarFuelType.Benzine,
                            Year = 2018,
                            Mileage = 68483,
                            EngineVolume = 3200,
                            Transmission = CarTransmission.Automatic,
                            Price = 400_000,
                        },
                        new Car()
                        {
                            Make = CarMake.Audi,
                            Type = CarType.Sedan,
                            Model = "A8",
                            Color = CarColor.White,
                            VIN = "123456789",
                            Images = new List<string>
                            {
                                "https://media.carsandbids.com/cdn-cgi/image/width=2080,quality=70/d9b636c2ec84ddc3bc7f2eb32861b39bdd5f9683/photos/rbAPeDR2.4MFH1uv1J-(edit).jpg?t=162843319241",
                            },
                            FuelType = CarFuelType.Benzine,
                            Year = 2020,
                            Mileage = 10000,
                            EngineVolume = 3000,
                            Transmission = CarTransmission.Automatic,
                            Price = 500_000,
                        },
                        new Car()
                        {
                            Make = CarMake.MercedesBenz,
                            Type = CarType.SUV,
                            Model = "GLC",
                            Color = CarColor.Black,
                            VIN = "987654321",
                            Images = new List<string>
                            {
                                "https://inv.assets.ansira.net/ChromeColorMatch/us/WHITE_cc_2024MBS680002_01_1280_040.jpg",
                            },
                            FuelType = CarFuelType.Benzine,
                            Year = 2021,
                            Mileage = 5000,
                            EngineVolume = 3500,
                            Transmission = CarTransmission.Automatic,
                            Price = 600_000,
                        },
                        new Car()
                        {
                            Make = CarMake.BMW,
                            Type = CarType.Sedan,
                            Model = "7 Series",
                            Color = CarColor.Blue,
                            VIN = "246813579",
                            Images = new List<string>
                            {
                                "https://cdn.motor1.com/images/mgl/9mmmOl/s3/2023-bmw-7-series-the-first-edition-for-japan.jpg",
                            },
                            FuelType = CarFuelType.Benzine,
                            Year = 2022,
                            Mileage = 2000,
                            EngineVolume = 4000,
                            Transmission = CarTransmission.Automatic,
                            Price = 700_000,
                        },
                        new Car()
                        {
                            Make = CarMake.Tesla,
                            Type = CarType.Sedan,
                            Model = "Model X",
                            Color = CarColor.Red,
                            VIN = "135792468",
                            Images = new List<string>
                            {
                                "https://issimi-vehicles-cdn.b-cdn.net/publicamlvehiclemanagement/VehicleDetails/109/1.jpg?width=3840&quality=75",
                            },
                            FuelType = CarFuelType.Electric,
                            Year = 2023,
                            Mileage = 1000,
                            Price = 800_000,
                            Transmission = CarTransmission.Automatic,
                        },
                        new Car()
                        {
                            Make = CarMake.Porsche,
                            Type = CarType.Sports,
                            Model = "911",
                            Color = CarColor.Yellow,
                            VIN = "864209753",
                            Images = new List<string>
                            {
                                "https://www.austinclearbra.com/wp-content/uploads/sites/6/2021/09/2018-Porsche-911-GT3-1-scaled.jpg",
                            },
                            FuelType = CarFuelType.Benzine,
                            Year = 2024,
                            Mileage = 500,
                            EngineVolume = 4500,
                            Transmission = CarTransmission.Manual,
                            Price = 900_000,
                        },
                        new Car()
                        {
                            Make = CarMake.Lamborghini,
                            Type = CarType.Sports,
                            Model = "Aventador",
                            Color = CarColor.Green,
                            VIN = "102938475",
                            Images = new List<string>
                            {
                                "https://media.gq-magazine.co.uk/photos/5d13ad1aeef92171f5a0072b/16:9/w_2560%2Cc_limit/aventador-04-gq-24aug18_b.jpg",
                            },
                            FuelType = CarFuelType.Benzine,
                            Year = 2020,
                            Mileage = 13244,
                            EngineVolume = 4500,
                            Transmission = CarTransmission.Automatic,
                            Price = 1_000_000,

                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
