using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; 
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

            modelBuilder.Entity<RollerCoaster>(entity =>
            {
                entity.HasKey(e => e.Name); 
                entity.OwnsOne(x => x.Ordinal);
            });

            modelBuilder.Entity<HauntedHouse>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.OwnsOne(x => x.Ordinal);
            });

            modelBuilder.Entity<DuckFishing>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.OwnsOne(x => x.Ordinal);
            });

            modelBuilder.Entity<GiftShop>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.OwnsOne(x => x.Ordinal);
            });

            modelBuilder.Entity<FoodShop>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.OwnsOne(x => x.Ordinal);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
