using Microsoft.EntityFrameworkCore;
using AuthenticationService.Models;

namespace ScreenOps.AuthenticationService.Data
{
    public class AppDBContext : DbContext
    {

        protected readonly IConfiguration Configuration;

        public AppDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("Database"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(b =>
            {
                b.Property(m => m.Role).HasConversion<string>();
            });
        }

        public DbSet<User> Users { get; set; }
    }
}
