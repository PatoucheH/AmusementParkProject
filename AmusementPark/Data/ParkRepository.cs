using AmusementPark.Services;
using AmusementPark.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace AmusementPark.Data
{
    public class ParkRepository
    {
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
            }
            else
            {
                await context.Parks.AddAsync(park);
            }

            await context.SaveChangesAsync();
            AnsiConsole.MarkupLine($"[green]✔ Park '{park.Name}' has been saved successfully![/]");
        }
    }
}
