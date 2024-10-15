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
    }
}
