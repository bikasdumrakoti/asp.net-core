using CitiesManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(new City()
            {
                CityID = Guid.Parse("A0258B84-381F-4866-AE7D-DBB01C42A275"),
                CityName = "New York"
            });
            modelBuilder.Entity<City>().HasData(new City()
            {
                CityID = Guid.Parse("44465505-6290-4ECD-B065-4F3A350281AC"),
                CityName = "London"
            });
        }
    }
}
