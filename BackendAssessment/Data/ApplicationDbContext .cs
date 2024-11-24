using BackendAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAssessment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adding mocked test user
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "123" }
            );
        }
    }
}
