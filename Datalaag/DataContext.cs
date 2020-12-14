using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;

namespace Datalaag
{
    public class DataContext : DbContext
    {
        public String dataString;

        public DataContext(String type = "test")
        {
            if (!type.Equals("test"))
                dataString = @"Data Source=WILLIAM-SLABBAE\SQLEXPRESS;Initial Catalog=Geo_service;Integrated Security=True";
            else
                dataString = @"Data Source=WILLIAM-SLABBAE\SQLEXPRESS;Initial Catalog=Geo_service;Integrated Security=True";
        }
        public DataContext()
        {
            dataString = @"Data Source=WILLIAM-SLABBAE\SQLEXPRESS;Initial Catalog=Geo_service;Integrated Security=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dataString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasOne(c => c.Continent).WithMany(c => c.Countries);
        }


        public virtual DbSet<City> CityData { get; set; }
        public virtual DbSet<Country> CountryData { get; set; }
        public virtual DbSet<River> RiverData { get; set; }
        public virtual DbSet<Continent> ContinentData { get; set; }
    }
}
