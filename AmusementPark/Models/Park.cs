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

        public void DisplayPark()
        {
            var table = new Table().Centered();
            table.Border = TableBorder.Rounded;
            table.ShowRowSeparators();
            int timeBewteenSpawn = 0;
            AnsiConsole.Write(new Rule("[teal] YOUR PARK [/]"));
            AnsiConsole.Live(table)
                .AutoClear(false)
                .Overflow(VerticalOverflow.Ellipsis)
                .Cropping(VerticalOverflowCropping.Top)
                .Start(ctx =>
                {
                    table.AddColumn("Y/X");
                    table.AddColumn("1");
                    table.AddColumn("2");
                    table.AddColumn("3");
                    table.AddColumn("4");
                    table.AddColumn("5");
                    table.AddRow("1", GridPark[0, 0], GridPark[0, 1], GridPark[0, 2], GridPark[0, 3], GridPark[0, 4]);
                    ctx.Refresh();
                    Thread.Sleep(timeBewteenSpawn);
                    table.AddRow("2", GridPark[1, 0], GridPark[1, 1], GridPark[1, 2], GridPark[1, 3], GridPark[1, 4]);
                    ctx.Refresh();
                    Thread.Sleep(timeBewteenSpawn);
                    table.AddRow("3", GridPark[2, 0], GridPark[2, 1], GridPark[2, 2], GridPark[2, 3], GridPark[2, 4]);
                    ctx.Refresh();
                    Thread.Sleep(timeBewteenSpawn);
                    table.AddRow("4", GridPark[3, 0], GridPark[3, 1], GridPark[3, 2], GridPark[3, 3], GridPark[3, 4]);
                    ctx.Refresh();
                    Thread.Sleep(timeBewteenSpawn);
                    table.AddRow("5", GridPark[4, 0], GridPark[4, 1], GridPark[4, 2], GridPark[4, 3], GridPark[4, 4]);
                    ctx.Refresh();
                    Thread.Sleep(timeBewteenSpawn);
                });
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
                string nameChoose = AnsiConsole.Ask<string>($"Which name do you want for your {building} : ");
                IBuilding buildingBuy = building switch
                {
                    "RollerCoaster" => new RollerCoaster(nameChoose),
                    "HauntedHouse" => new HauntedHouse(nameChoose),
                    "GiftShop" => new GiftShop(nameChoose),
                    "FoodShop" => new FoodShop(nameChoose),
                    "DuckFishing" => new DuckFishing(nameChoose),
                    _ => throw new Exception("Unknown Type")
                };

                InventoryBuildings.Add(buildingBuy);
                Budget -= (double)buildingBuy.Price;
                AnsiConsole.MarkupLine($"[green]You successfully bought and add your new {buildingBuy.Name} to your inventory[/]\n Your budget : {Budget}");
            }
        }


        public void PlaceSomeBuilding()
        {
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
            AnsiConsole.MarkupLine("[blue] Your building place : [/]");
            foreach (var building in PlacedBuilding)
            {
                AnsiConsole.MarkupLine($"{building.Name}");
            }
            string chooseName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the building you want to [red]remove[/] in your park : "));

            IBuilding chooseBuilding = PlacedBuilding.FirstOrDefault(b => b.Name == chooseName);
            Position point = chooseBuilding.Ordinal;

            GridPark[point.X, point.Y] = ":green_square:";
            InventoryBuildings.Add(chooseBuilding);
            PlacedBuilding.Remove(chooseBuilding);
            AnsiConsole.MarkupLine($"[green]You successfully removed {chooseBuilding.Name} from your park[/]");
        }
    }
}
