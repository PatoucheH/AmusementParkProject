using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace AmusementPark.Data
{
    public static class DataAccess
    {
        public static async Task InitializeDatabaseAsync()
        {
            using var context = new ParkContext();
            var created = await context.Database.EnsureCreatedAsync();

            var basePath = AppContext.BaseDirectory;
            var projectRoot = Path.GetFullPath(Path.Combine(basePath, "..", "..", ".."));
            var dbPath = Path.Combine(projectRoot, "Data", "park.db");

            if (created)
                Console.WriteLine($"Database created at: {dbPath}");
            else
                Console.WriteLine("Database already exists.");
        }

    }
}


