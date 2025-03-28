using Microsoft.EntityFrameworkCore;
using PersonApi.Models;

namespace PersonApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Person> Persons { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed some initial data
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890",
                    Address = "123 Main St, City",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    CreatedAt = DateTime.UtcNow
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Phone = "987-654-3210",
                    Address = "456 Oak Ave, Town",
                    DateOfBirth = new DateTime(1990, 8, 22),
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}