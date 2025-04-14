using Microsoft.EntityFrameworkCore;
using ScreeningsService.Models;

namespace ScreeningsService.Data
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
            modelBuilder.Entity<Screening>(b =>
            {
                b.HasIndex(x => x.MovieId);
                b.HasIndex(x => x.RoomId);
                b.HasIndex(x => x.CinemaId);
                b.HasIndex(x => x.Status);
            });
            modelBuilder.Entity<ScreeningSchedule>(b =>
            {
                b.HasIndex(x => x.MovieId);
                b.HasIndex(x => x.RoomId);
                b.HasIndex(x => x.CinemaId);
                b.HasIndex(x => x.Status);
            });
        }

        public DbSet<Screening> Screenings { get; set; }
        public DbSet<ScreeningSchedule> ScreeningSchedules { get; set; }

    }
}
