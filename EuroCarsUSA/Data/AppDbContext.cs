using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EuroCarsUSA.Models.Form;

namespace EuroCarsUSA.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CustomOrder> CustomOrders { get; set; }
        public DbSet<FormCarMake> FormCarMakes { get; set; }

        public DbSet<DetailPageForm> DetailPageForms { get; set; }

        public DbSet<CarStatistic> CarStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarStatistic>()
                .HasOne(cs => cs.Car)
                .WithMany()
                .HasForeignKey(cs => cs.CarId);

            modelBuilder.Entity<Form>()
                .HasOne(f => f.CustomOrder)
                .WithMany(co => co.Forms)
                .HasForeignKey(f => f.CustomOrderId);

            modelBuilder.Entity<FormCarMake>()
                .HasOne(fcm => fcm.Form)
                .WithMany(f => f.FormCarMakes)
                .HasForeignKey(fcm => fcm.FormId);

            modelBuilder.Entity<FormCarMake>()
                 .Property(fcm => fcm.CarMake)
                 .HasConversion<int>();

            modelBuilder.Entity<FormCarType>()
                .HasOne(fct => fct.Form)
                .WithMany(f => f.FormCarTypes)
                .HasForeignKey(fct => fct.FormId);

            modelBuilder.Entity<FormCarType>()
                .Property(fct => fct.CarType)
                .HasConversion<int>();

            modelBuilder.Entity<FormCarColor>()
                .HasOne(fct => fct.Form)
                .WithMany(f => f.FormCarColors)
                .HasForeignKey(fct => fct.FormId);

            modelBuilder.Entity<FormCarColor>()
                .Property(fct => fct.CarColor)
                .HasConversion<int>();

            modelBuilder.Entity<FormCarFuelType>()
                .HasOne(fct => fct.Form)
                .WithMany(f => f.FormCarFuelTypes)
                .HasForeignKey(fct => fct.FormId);

            modelBuilder.Entity<FormCarFuelType>()
                .Property(fct => fct.CarFuelType)
                .HasConversion<int>();

            modelBuilder.Entity<FormCarTransmission>()
               .HasOne(fct => fct.Form)
               .WithMany(f => f.FormCarTransmissions)
               .HasForeignKey(fct => fct.FormId);

            modelBuilder.Entity<FormCarTransmission>()
                .Property(fct => fct.CarTransmission)
                .HasConversion<int>();
        }
    }
}
