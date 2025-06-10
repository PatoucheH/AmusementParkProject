using AmusementPark.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Services
{
    public class ParkService
    {
        /// <summary>
        /// Allows the user to purchase one or more buildings for their park by selecting from a predefined list of
        /// building types.
        /// </summary>
        /// <exception cref="Exception">Thrown if an unknown building type is selected.</exception>
        public static List<(string, string)> BuySomeBuilding(Park park)
        {
            List<(string type, string name)> buildings = new();
            var choices = AnsiConsole.Prompt(new MultiSelectionPrompt<string>()
                .Title("Choose the type of building you want to buy :")
                .AddChoices(new[]
                {
                    "RollerCoaster",
                    "HauntedHouse",
                    "GiftShop",
                    "FoodShop",
                    "DuckFishing"
                }));

            foreach (var type in choices)
            {
                string name = string.Empty;
                while (string.IsNullOrEmpty(name) )
                {
                    name = AnsiConsole.Prompt(new TextPrompt<string>($"Enter the name of the {type} you [green]bought[/] for your park : "));
                    if(park.PlacedBuilding.FirstOrDefault(b => b.Name == name) != null || park.InventoryBuildings.FirstOrDefault(b => b.Name == name) != null)
                    {
                        AnsiConsole.MarkupLine($"[red]{name} is already taken ! [/]");
                        name = string.Empty;
                    }
                }
                buildings.Add((type, name));
            }
            return buildings;
        }

        /// <summary>
        /// Places a building from the inventory onto the park grid.
        /// </summary>
        public static void PlaceSomeBuilding(Park park)
        {
            if (park.InventoryBuildings.Count <= 0)
            {
                AnsiConsole.MarkupLine("[red]Your inventory is empty please buy some buildings before ![/]");
                return;
            }
            park.DisplayInventory();

            string chooseName = string.Empty;
            while (!park.InventoryBuildings.Any(build => build.Name == chooseName))
            {
                chooseName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the building you want to [green]place[/] in your park (exit to leave) : "));
                if (chooseName == "exit") return;
            }

            var x = AnsiConsole.Prompt(new TextPrompt<int>("Choose the X value for your building : ")
                .AddChoices(new[] { 1, 2, 3, 4, 5 }));
            var y = AnsiConsole.Prompt(new TextPrompt<int>("Choose the Y value for your building : ")
                .AddChoices(new[] { 1, 2, 3, 4, 5 }));
            var success = park.TryPlaceBuilding(chooseName, x, y, out string message);

            if (success)
                AnsiConsole.MarkupLine($"[green]{message}[/]");
            else
            {
                AnsiConsole.MarkupLine($"[red]{message}[/]");
                var retry = AnsiConsole.Prompt(new TextPrompt<bool>("Would you continue ? ")
                        .AddChoice(true)
                        .AddChoice(false)
                        .DefaultValue(true)
                        .WithConverter(choice => choice ? "y" : "n"));
                if (retry) PlaceSomeBuilding(park);
            }
        }

        /// <summary>
        /// Removes a building from the park based on the user's input.
        /// </summary>
        public static void RemoveSomeBuilding(Park park)
        {
            if (park.PlacedBuilding.Count <= 0)
            {
                AnsiConsole.MarkupLine("[red]Your parc is empty please place some buildings before ! [/]");
                return;
            }
            AnsiConsole.MarkupLine("[blue] Your building place : [/]");
            foreach (var building in park.PlacedBuilding)
            {
                AnsiConsole.MarkupLine($"- {building.Name}\n\tDesc : {building.Description}");
            }
            string chooseName = string.Empty;

            while (!park.PlacedBuilding.Any(b => b.Name == chooseName) || string.IsNullOrEmpty(chooseName))
            {
                chooseName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the building you want to [red]remove[/] in your park (exit to leave) : "));
                if (chooseName == "exit") return;
            }

            bool success = park.TryRemoveBuilding(chooseName, out string message);
            if (success)
                AnsiConsole.MarkupLine($"[green]{message}[/]");
            else
                AnsiConsole.MarkupLine($"[red]{message}[/]");
        }


        public static void DisplayPopularityAttraction(Park park)
        {
            foreach (IBuilding attraction in park.PlacedBuilding)
            {
                double inAttractrion = attraction.VisitorInAttraction;
                double totalPark = park.TotalVisitors;

                var pourcent =(inAttractrion/totalPark)*100;

                AnsiConsole.MarkupLine($"A total of {inAttractrion} visitors have visited {attraction.Name}");
            }
        }
    }
}
