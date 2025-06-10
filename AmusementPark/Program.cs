using AmusementPark.Data;
using AmusementPark.Models;
using AmusementPark.Services;
using AmusementPark.Utils;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using Spectre.Console.Extensions;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmusementPark
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var duck = new DuckFishing("Test Duck") { Ordinal = new Position(5, 4) };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new PositionJsonConverter());
            options.Converters.Add(new BuildingJsonConverter());

            var json = JsonSerializer.Serialize<IBuilding>(duck, options);
            Console.WriteLine("JSON GÉNÉRÉ :");
            Console.WriteLine(json);

            // Test désérialisation
            var rebuilt = JsonSerializer.Deserialize<IBuilding>(json, options);
            if (rebuilt is DuckFishing df)
                Console.WriteLine($"✔ Ordinal après lecture : {df.Ordinal?.X}, {df.Ordinal?.Y}");




            // Make emoji's works on terminal
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Create Database or initialise it 
            await DataAccess.InitializeDatabaseAsync();

            // instantie ParkRepository
            var repository = new ParkRepository();

            // let user choose if he wants to load a park or create a new one 
            var choices = new List<string> { "Create a new park" };
            choices.AddRange(await repository.GetAllParkNamesAsync());

            var choiceGame = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("choose an option :")
                    .PageSize(10)
                    .AddChoices(choices));

            // instantie the park
            Park YourPark;

            // if user choose to create a new one 
            if (choiceGame == "Create a new park")
            {
                string name = AnsiConsole.Prompt(
                    new TextPrompt<string>("Choose the name of your park :")
                        .Validate(name =>
                            name.Length switch
                            {
                                < 2 => ValidationResult.Error("[red]Name too short[/]"),
                                > 20 => ValidationResult.Error("[red]Name too long[/]"),
                                _ => ValidationResult.Success(),
                            }));

                YourPark = new Park(name);
            }
            // if user choose to load a park
            else
            {
                YourPark = await repository.LoadParkAsync(choiceGame)
                    ?? throw new Exception("Error when charging the park.");
            }

            // Create databBase 

            await DataAccess.InitializeDatabaseAsync();

            // Clear the console
            Console.Clear();

            // create var to stop the refresh when we are in a prompt
            bool inMenu = false;
            var cancelToken = new CancellationTokenSource();

            // create the back loop to generate visitors and money and display each 
            _ = Task.Run(async () =>
                {
                    while (true)
                    {
                        EarnMoney.GenerateMoneyAndVisitors(YourPark);
                        if (!inMenu)
                        {
                            Console.Clear();
                            AnsiConsole.Write(new FigletText(YourPark.Name).Centered().Color(Color.Lime));
                            AnsiConsole.Write(YourPark.DisplayPark());
                        }
                        await Task.Delay(5_000);
                    }
                });
            // initialise the user's choice
            string choice = string.Empty;

            while (choice != "6")
            {
                // Ask user to make a choice of what he wants to do 
                choice = Menu.DisplayMenu().Substring(0, 1);

                // Chekc if the user's budget is not too low if it is he lost the game ! 
                if (YourPark.Budget < -5000)
                {
                    AnsiConsole.Write(new FigletText($"YOU LOSE ASSHOLE !!! ").Centered().Color(Color.Red));
                    return;
                }

                // Switch which make an action in realtion to the user's choice
                switch (choice)
                {
                    case "1":
                        YourPark.DisplayInventory();
                        break;
                    case "2":
                        inMenu = true;
                        ParkService.PlaceSomeBuilding(YourPark);
                        inMenu = false;
                        break;
                    case "3":
                        inMenu = true;
                        ParkService.RemoveSomeBuilding(YourPark);
                        inMenu = false;
                        break;
                    case "4":
                        inMenu = true;
                        YourPark.BuyBuilding(ParkService.BuySomeBuilding());
                        inMenu = false;
                        break;
                    case "5":
                        inMenu = true;

                        await repository.SaveParkAsync(YourPark);
                        inMenu = false;
                        break;
                    case "6":
                        AnsiConsole.Markup("[darkred]You exit the game. Thank you ![/]");
                        break;
                    default:
                        AnsiConsole.Markup("[red]You entered a wrong input please choose a number in the menu.[/]");
                        break;
                }
            }
        }
    }
}
