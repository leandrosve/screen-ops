using CinemasService.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemasService.Data
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

            modelBuilder.Entity<LayoutElement>(b =>
            {
                b.Property(m => m.Type).HasConversion<string>();
                b.HasIndex(m => m.LayoutId);
                b.HasIndex(m => m.PositionY);
                b.HasIndex(m => m.PositionX);

            });
           
            modelBuilder.Entity<Layout>(b =>
            {
                b.HasIndex(u => u.Name).IsUnique();
            });
        }

        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<LayoutElement> LayoutElements { get; set; }
    }
}
