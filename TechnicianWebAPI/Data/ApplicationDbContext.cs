using Microsoft.EntityFrameworkCore;
using TmsWebApi.Models;

namespace TmsWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tasks Table Configuration
            modelBuilder.Entity<Tasks>()
                .HasKey(t => t.Id); // Define Primary Key

            // Notifications Table Configuration
            modelBuilder.Entity<Notification>()
                .HasKey(n => n.Id); // Define Primary Key

            // Users Table Configuration
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Define Primary Key

            base.OnModelCreating(modelBuilder);
        }
    }
}
