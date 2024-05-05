using EuroCarsUSA.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroCarsUSA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DetailPageForm> DetailPageForms { get; set; }
    }
}
