
using AmusementPark.Models;
using AmusementPark.Services;
using AmusementPark.Utils;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using Spectre.Console.Extensions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmusementPark
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Make emoji's works on terminal
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Title to welcoming in our game
            AnsiConsole.Write(new FigletText("Welcome to our new manager roller coaster game !").Centered().Color(Color.Teal));

            // create the park
            Park YourPark;

            string newOrSaveGame = string.Empty;
            while (newOrSaveGame is null || newOrSaveGame != "1" || newOrSaveGame != "2")
            {
                newOrSaveGame = Menu.NewOrSaveGame().Substring(0, 1);
            }

            if(newOrSaveGame == "1")
            {
                // Ask the user to choose his park's name
                string Name = AnsiConsole.Prompt(new TextPrompt<string>("Choose the name of your park : ")
                    .Validate((name) => name.Length switch
                {
                    < 2 => ValidationResult.Error("[red]Your park name is too low ! [/]"),
                    > 20 => ValidationResult.Error("[red]Your park name is too long ! [/]"),
                    _ => ValidationResult.Success(),
                }));
                YourPark = new Park(Name);
            }
            else if (newOrSaveGame == "2")
            {
                
            }
            else
            {
                // Ask the user to choose his park's name
                string Name = AnsiConsole.Prompt(new TextPrompt<string>("Choose the name of your park : ")
                    .Validate((name) => name.Length switch
                    {
                        < 2 => ValidationResult.Error("[red]Your park name is too low ! [/]"),
                        > 20 => ValidationResult.Error("[red]Your park name is too long ! [/]"),
                        _ => ValidationResult.Success(),
                    }));
                YourPark = new Park(Name);
            }

            // Clear the console
            Console.Clear();
            // Display the user's park's name
            AnsiConsole.Write(new FigletText(YourPark.Name).Centered().Color(Color.Lime));

            // create var to stop the refresh when we are in a prompt
            bool inMenu = false;
            var cancelToken = new CancellationTokenSource();

            // create the back loop to generate visitors and money and display each x
            _ = Task.Run(async () =>
                {
                    while (true)
                    {
                        EarnMoney.GenerateMoneyAndVisitors(YourPark);
                        if (!inMenu)
                        {
                            Console.Clear();
                            AnsiConsole.Write(new FigletText(Name).Centered().Color(Color.Lime));
                            AnsiConsole.Write(YourPark.DisplayPark());
                        }
                        await Task.Delay(5_000);
                    }
                });
            // initialise the user's choice
            string choice = string.Empty;

            while (choice != "5")
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
                        YourPark.PlaceSomeBuilding();
                        inMenu = false;
                        break;
                    case "3":
                        inMenu = true;
                        YourPark.RemoveSomeBuilding();
                        inMenu = false;
                        break;
                    case "4":
                        inMenu = true;
                        YourPark.BuySomeBuilding();
                        inMenu = false;
                        break;
                    case "5":
                        new ParkRepository("Data Source=park.db").SavePark(YourPark);
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
