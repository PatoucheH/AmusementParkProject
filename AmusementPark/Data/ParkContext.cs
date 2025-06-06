using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmusementPark.Models;
using Microsoft.EntityFrameworkCore;


namespace AmusementPark.Data
{
    internal class ParkContext : DbContext
    {
        public DbSet<Park> Parks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var basePath = AppContext.BaseDirectory;
            var projectRoot = Path.GetFullPath(Path.Combine(basePath,"..", "..", "..")); 
            var dbPath = Path.Combine(projectRoot, "Data", "park.db");

            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

            options.UseSqlite($"Data Source={dbPath}");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Park>()
                .Ignore(p => p.InventoryBuildings)
                .Ignore(p => p.PlacedBuilding)
                .Ignore(p => p.GridPark);
        }
    }
}
