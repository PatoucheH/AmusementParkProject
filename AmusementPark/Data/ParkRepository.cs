using AmusementPark.Services;
using AmusementPark.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace AmusementPark.Data
{
    public class ParkRepository
    {

        /// <summary>
        /// Save the park actual in the database
        /// </summary>
        /// <param name="park">The actual park</param>
        public async Task SaveParkAsync(Park park)
        {
            using var context = new ParkContext();

            var existingPark = await context.Parks
                .FirstOrDefaultAsync(p => p.Name == park.Name);

            if (existingPark != null)
            {
                existingPark.Budget = park.Budget;
                existingPark.TotalVisitors = park.TotalVisitors;
                existingPark.InventoryBuildingsJson = park.InventoryBuildingsJson;
                existingPark.PlacedBuildingJson = park.PlacedBuildingJson;
                existingPark.GridParkJson = park.GridParkJson;
            }
            else
            {
                await context.Parks.AddAsync(park);
            }

            await context.SaveChangesAsync();
            AnsiConsole.MarkupLine($"[green]✔ Park '{park.Name}' has been saved successfully![/]");
        }

        /// <summary>
        /// Load a park in the database
        /// </summary>
        /// <param name="parkName">the park that you want to load from the database</param>
        public async Task<Park?> LoadParkAsync(string parkName)
        {
            using var context = new ParkContext();
            return await context.Parks.FirstOrDefaultAsync(p => p.Name == parkName);
        }

        /// <summary>
        /// Get the name of all the park save in the database
        /// </summary>
        public async Task<List<string>> GetAllParkNamesAsync()
        {
            using var context = new ParkContext();
            return await context.Parks.Select(p => p.Name).ToListAsync();
        }

    }
}
