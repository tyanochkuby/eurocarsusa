using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EuroCarsUSA.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<DetailPageForm> DetailPageForms { get; set; }

        public DbSet<CarStatistic> CarStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarStatistic>()
                .HasOne(cs => cs.Car)
                .WithMany()
                .HasForeignKey(cs => cs.CarId);
        }
    }
}
