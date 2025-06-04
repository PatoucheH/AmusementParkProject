using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AmusementPark.Models
{
    public class Park
    {
        public string ParkName { get; set; }
        public double Budget { get; set; } = 50_000;
        public int VisitorsEntry {  get; set; }
        public int VisitorsOut {  get; set; }
        public int TotalVisitors {get; set ;}
        List<IBuilding> InventoryBuildings { get; set; } = new();
        public List<IBuilding> PlacedBuilding { get; set; } = new();
        string[,] GridPark { get; set; } =
        {
            {":green_square:",":green_square:",":green_square:",":green_square:",":green_square:" },
            {":green_square:",":green_square:",":green_square:",":green_square:",":green_square:" },
            {":green_square:",":green_square:",":green_square:",":green_square:",":green_square:" },
            {":green_square:",":green_square:",":green_square:",":green_square:",":green_square:" },
            {":green_square:",":green_square:",":green_square:",":green_square:",":green_square:" }
        };

        public Park(string name)
        {
            ParkName = name;
        }
         public Grid DisplayPark()
        {
            var grid = new Grid();
            grid.AddColumn();
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
                .AddItem("Visitors in the park", VisitorsEntry - VisitorsOut, Color.Teal)
                .AddItem("Total visitors", TotalVisitors, Color.Lime);

            grid.AddRow(rule);
            grid.AddRow(table);
            grid.AddEmptyRow();
            grid.AddRow(barChart);
            return grid;
        }

        public void DisplayInventory()
        {
            AnsiConsole.MarkupLine("[navy]Your inventory : [/]");
            foreach (var building in InventoryBuildings)
            {
                AnsiConsole.MarkupLine($"[blue]{building.Name}[/]");
                if(building.Description is not null) AnsiConsole.MarkupLine($"[yellow]{building.Description}[/]");
            }
        }

        public void BuySomeBuilding()
        {
            List<string> buildingChoose = AnsiConsole.Prompt(new MultiSelectionPrompt<string>()
                .Title("Choose the type of building you want to buy :")
                .AddChoices(new[]
                {
                    "RollerCoaster",
                    "HauntedHouse",
                    "GiftShop",
                    "FoodShop",
                    "DuckFishing"
                }));

            foreach (var building in buildingChoose)
            {
                string chooseName = string.Empty;

                while (string.IsNullOrEmpty(chooseName))
                {
                    chooseName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the building you want to [red]remove[/] in your park : "));
                }
                IBuilding buildingBuy = building switch
                {
                    "RollerCoaster" => new RollerCoaster(chooseName),
                    "HauntedHouse" => new HauntedHouse(chooseName),
                    "GiftShop" => new GiftShop(chooseName),
                    "FoodShop" => new FoodShop(chooseName),
                    "DuckFishing" => new DuckFishing(chooseName),
                    _ => throw new Exception("Unknown Type")
                };

                InventoryBuildings.Add(buildingBuy);
                Budget -= (double)buildingBuy.Price;
                AnsiConsole.MarkupLine($"[green]You successfully bought and add your new {buildingBuy.Name} to your inventory[/]\n Your budget : {Budget}");
            }
        }


        public void PlaceSomeBuilding()
        {
            if (InventoryBuildings.Count <= 0 )
            {
                AnsiConsole.MarkupLine("[red]Your inventory is empty please buy some buildings before ![/]");
                return;
            }
            DisplayInventory();
            string chooseName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the building you want to [green]place[/] in your park : "));
            while(!InventoryBuildings.Any(build => build.Name == chooseName))
            {
                chooseName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the building you want to [green]place[/] in your park : "));
            }
            IBuilding chooseBuilding = InventoryBuildings.FirstOrDefault(b => b.Name == chooseName);

            var x = AnsiConsole.Prompt(new TextPrompt<int>("Choose the X value for your building : ")
                .AddChoices(new[] { 1, 2, 3, 4, 5 }));
            var y = AnsiConsole.Prompt(new TextPrompt<int>("Choose the Y value for your building : ")
                .AddChoices(new[] {1, 2, 3, 4, 5 }));

            Position Point = new(x - 1, y - 1);
            if (GridPark[Point.X, Point.Y] == ":green_square:")
            {
                chooseBuilding.Ordinal = Point;

                GridPark[chooseBuilding.Ordinal.X, chooseBuilding.Ordinal.Y] = chooseBuilding.Emoji;
                InventoryBuildings.Remove(chooseBuilding);
                PlacedBuilding.Add(chooseBuilding);
                AnsiConsole.MarkupLine($"[green]You successfully placed your {chooseBuilding.Name}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]You cannot placed your {chooseBuilding.Name} in this place, there is already something [/]");
                var confirmation = AnsiConsole.Prompt(new TextPrompt<bool>("Would you continue ? ")
                        .AddChoice(true)
                        .AddChoice(false)
                        .DefaultValue(true)
                        .WithConverter(choice => choice ? "y" : "n"));
                if (confirmation) PlaceSomeBuilding();
                else return;

            }
        }



        public void RemoveSomeBuilding()
        {
            if(PlacedBuilding.Count <= 0)
            {
                AnsiConsole.MarkupLine("[red]Your parc is empty please place some buildings before ! [/]");
                return;
            }
            AnsiConsole.MarkupLine("[blue] Your building place : [/]");
            foreach (var building in PlacedBuilding)
            {
                AnsiConsole.MarkupLine($"{building.Name}");
            }
            string chooseName = string.Empty;

            while(!PlacedBuilding.Any(b => b.Name == chooseName) || string.IsNullOrEmpty(chooseName))
            {
                chooseName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the building you want to [red]remove[/] in your park : "));
            }

            IBuilding chooseBuilding = PlacedBuilding.FirstOrDefault(b => b.Name == chooseName);
            Position point = chooseBuilding.Ordinal;

            GridPark[point.X, point.Y] = ":green_square:";
            InventoryBuildings.Add(chooseBuilding);
            PlacedBuilding.Remove(chooseBuilding);
            AnsiConsole.MarkupLine($"[green]You successfully removed {chooseBuilding.Name} from your park[/]");
        }
    }
}
 