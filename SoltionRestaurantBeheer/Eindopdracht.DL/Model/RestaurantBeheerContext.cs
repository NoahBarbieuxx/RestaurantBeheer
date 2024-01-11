using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Model
{
    public class RestaurantBeheerContext : DbContext
    {
        private readonly string _connectionString;

        public RestaurantBeheerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<GebruikerEF> Gebruikers { get; set; }
        public DbSet<ReservatieEF> Reservaties { get; set; }
        public DbSet<RestaurantEF> Restaurants { get; set; }
        public DbSet<TafelEF> Tafels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}