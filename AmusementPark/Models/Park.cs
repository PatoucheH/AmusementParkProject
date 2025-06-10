using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AmusementPark.Services;
using AmusementPark.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace AmusementPark.Models
{
    /// <summary>
    /// Main class which manages all the parks with all his methods
    /// </summary>
    public class Park
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; } = 50_000;
        public int VisitorsEntry {  get; set; }
        public int VisitorsOut {  get; set; }
        public int VisitorInPark { get; set; } = 0;
        public int TotalVisitors {get; set ;}
        [NotMapped]
        public string[,] GridPark { get; set; } 
        [NotMapped]
        public List<IBuilding> InventoryBuildings { get; set; } = new();
        [NotMapped]
        public List<IBuilding> PlacedBuilding { get; set; } = new();

        // Serialization Db

        public string? GridParkJson
        {
            get => GridJson.SerializeGrid(GridPark);
            set => GridPark = GridJson.DeserializeGrid(value) ?? GridJson.GetDefaultGrid();
        }


        public string InventoryBuildingsJson
        {
            get => JsonSerializer.Serialize(InventoryBuildings, new JsonSerializerOptions { WriteIndented = false, Converters = { new BuildingJsonConverter() } });
            set => InventoryBuildings = JsonSerializer.Deserialize<List<IBuilding>>(value, new JsonSerializerOptions { Converters = { new BuildingJsonConverter() } }) ?? new();
        }

        public string PlacedBuildingJson
        {
            get => JsonSerializer.Serialize(PlacedBuilding, new JsonSerializerOptions { WriteIndented = false, Converters = { new BuildingJsonConverter() } });
            set => PlacedBuilding = JsonSerializer.Deserialize<List<IBuilding>>(value, new JsonSerializerOptions { Converters = { new BuildingJsonConverter() } }) ?? new();
        }

        //Constructor
        public Park(string name)
        {
            Name = name;
            GridPark = GridJson.GetDefaultGrid();
        }

        /// <summary>
        /// Displays a visual representation of the park, including a grid layout, statistics, and a summary.
        /// </summary>
        /// <returns>A <see cref="Grid"/> object containing the park's layout, visitor statistics, and budget information.</returns>
         public Grid DisplayPark()
        {
            var rule = new Rule("[teal]YOUR PARK[/]");
            var table = new Table();
            table.Border = TableBorder.Rounded;
            table.ShowRowSeparators();
            table.AddColumn("Y/X");
            table.AddColumn("1");
            table.AddColumn("2");
            table.AddColumn("3");
            table.AddColumn("4");
            table.AddColumn("5");
            for (int i = 0; i < 5; i++)
            {
                var row = new List<string> { (i + 1).ToString() };
                for (int j = 0; j < 5; j++)
                    row.Add(GridPark[i, j]);
                table.AddRow(row.ToArray());
            }

            var barChart = new BarChart()
                .Width(50)
                .Label("[lime bold underline] Your statistics[/]")
                .CenterLabel()
                .AddItem("Budget", Budget, Color.Green)
                .AddItem("Visitors in the park", VisitorInPark, Color.Teal)
                .AddItem("Total visitors", TotalVisitors, Color.Lime);

            var grid = new Grid();
            grid.AddColumn();
            grid.AddRow(rule);
            grid.AddRow(table);
            grid.AddEmptyRow();
            grid.AddRow(barChart);
            return grid;
        }

        /// <summary>
        /// Displays the list of buildings in the inventory, including their names and descriptions.
        /// </summary>
        public void DisplayInventory()
        {
            AnsiConsole.MarkupLine("[navy]Your inventory : [/]");
            foreach (var building in InventoryBuildings)
            {
                AnsiConsole.MarkupLine($"[blue]- {building.Name}[/]");
                if(building.Description is not null) AnsiConsole.MarkupLine($"Desc : [dodgerblue1]{building.Description}[/]");
            }
        }

        /// <summary>
        /// Purchases a collection of buildings and adds them to the inventory.
        /// </summary>
        /// <param name="buildings">A list of tuples where each tuple contains the type and name of a building to purchase. The type must be one
        /// of the predefined building types, such as "RollerCoaster", "HauntedHouse",  "GiftShop", "FoodShop", or
        /// "DuckFishing".</param>
        /// <exception cref="Exception">Thrown if a building type in the <paramref name="buildings"/> list is not recognized.</exception>
        public void BuyBuilding(List<(string, string)> buildings)
        {
            foreach (var (type, name) in buildings)
            {
                IBuilding buildingBuy = type switch
                {
                    "RollerCoaster" => new RollerCoaster(name),
                    "HauntedHouse" => new HauntedHouse(name),
                    "GiftShop" => new GiftShop(name),
                    "FoodShop" => new FoodShop(name),
                    "DuckFishing" => new DuckFishing(name),
                    _ => throw new Exception("Unknown Type")
                };

                InventoryBuildings.Add(buildingBuy);
                Budget -= (double)buildingBuy.Price;
            }
        }

        /// <summary>
        /// Attempts to place a building at the specified coordinates within the grid.
        /// </summary>
        /// <param name="name">The name of the building to place. Must match the name of a building in the inventory.</param>
        /// <param name="x">The X-coordinate of the target cell in the grid. Must be a valid grid position.</param>
        /// <param name="y">The Y-coordinate of the target cell in the grid. Must be a valid grid position.</param>
        /// <param name="message">An output parameter that contains a message describing the result of the operation.</param>
        /// <returns><see langword="true"/> if the building was successfully placed; otherwise, <see langword="false"/>.</returns>
        public bool TryPlaceBuilding(string name, int x, int y, out string message)
        {
            message = string.Empty;
            var building = InventoryBuildings.FirstOrDefault(b => b.Name == name);
            if (building == null)
            {
                message = $"Building with name {name} not found.";
                return false;
            }
            Position point = new(x - 1, y - 1);
            if (GridPark[point.X, point.Y] != ":green_square:")
            {
                message = $"The cell ({x},{y}) is already occupied.";
                return false;
            }

            building.Ordinal = point;
            GridPark[point.X, point.Y] = building.Emoji;
            InventoryBuildings.Remove(building);
            PlacedBuilding.Add(building);
            message = $"Successfully placed {building.Name} at ({x},{y})";
            return true;
        }

        /// <summary>
        /// Attempts to remove a building with the specified name from the park.
        /// </summary>
        /// <param name="name">The name of the building to remove. Cannot be null or empty.</param>
        /// <param name="message">An output parameter that contains a message describing the result of the operation. If the building is
        /// successfully removed, the message will indicate success. If the building is not found, the message will
        /// indicate the failure reason.</param>
        /// <returns><see langword="true"/> if the building was successfully removed; otherwise, <see langword="false"/>.</returns>
        public bool TryRemoveBuilding(string name, out string message)
        {
            message = string.Empty;
            var building = PlacedBuilding.FirstOrDefault(b => b.Name == name);
            if (building == null)
            {
                message = $"Building {name} not found in placed buildings.";
                return false;
            }
            Position point = building.Ordinal;

            GridPark[point.X, point.Y] = ":green_square:";
            PlacedBuilding.Remove(building);
            InventoryBuildings.Add(building);
            message = $"Successfully removed {name} from the park.";
            return true;
        }
    }
}
 