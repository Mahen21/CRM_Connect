using Microsoft.EntityFrameworkCore;  // for EF Core
using CRMConnect.Models;  // for accessing model classes

namespace CRMConnect.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for each model
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesActivity> SalesActivities { get; set; }

        public DbSet<CustomerTask> CustomerTasks { get; set; }

        public DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships between entities
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.SalesActivities)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.CommunicationLogs)
                .WithOne(cl => cl.Customer)
                .HasForeignKey(cl => cl.CustomerId);

            modelBuilder.Entity<CommunicationLog>()
                .HasOne(cl => cl.User)
                .WithMany()
                .HasForeignKey(cl => cl.UserId);
        }
    }
}
