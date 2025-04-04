using Microsoft.EntityFrameworkCore;
using MoviesService.Models;
using MoviesService.Utils;

namespace ScreenOps.MoviesService.Data
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

            modelBuilder.Entity<MovieMedia>(b =>
            {
                b.Property(m => m.Type).HasConversion<string>();
            });

            var genres = JsonLoader.LoadGenres();
            modelBuilder.Entity<Genre>(b => {
                b.HasData(genres);
            });
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }


    }
}
