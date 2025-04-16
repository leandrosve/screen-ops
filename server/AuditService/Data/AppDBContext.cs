using AuditService.Models;
using Microsoft.EntityFrameworkCore;

namespace ScreenOps.CinemasService.Data
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

            modelBuilder.Entity<AuditLog>(b =>
            {
                b.HasIndex(m => m.UserId);
                b.HasIndex(m => m.EntityType);
                b.HasIndex(m => m.EntityGuid);
                b.HasIndex(m => m.EntityId);
                b.HasIndex(m => m.Timestamp);
                b.HasIndex(m => m.IpAddress);
            });
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
       
    }
}
