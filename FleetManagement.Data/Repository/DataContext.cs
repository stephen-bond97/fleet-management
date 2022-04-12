using FleetManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Data.Repository
{
    public class DataContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<MOTRecord> MOTRecords { get; set; }

        public DbSet<User> Users { get; set; }
    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=data.db");
        }

        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
