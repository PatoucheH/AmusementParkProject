

using AmusementPark.Services;
using AmusementPark.Models;
using AmusementPark.Utils;
using Spectre.Console;
using System.Threading.Tasks;
using Spectre.Console.Extensions;

namespace AmusementPark
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            AnsiConsole.Write(new FigletText("Welcome to our new game !").Centered().Color(Color.Teal));
            string Name = AnsiConsole.Prompt(new TextPrompt<string>("Choose the name of your park : ")
                .Validate((name) => name.Length switch
            {
                < 2 => ValidationResult.Error("[red]Your park name is too low ! [/]"),
                > 20 => ValidationResult.Error("[red]Your park name is too long ! [/]"),
                _ => ValidationResult.Success(),
            }));
            Park YourPark = new Park(Name);

            Console.Clear();
            AnsiConsole.Write(new FigletText(Name).Centered().Color(Color.Lime));

            bool inMenu = false;
            var cancelToken = new CancellationTokenSource();

            _= Task.Run(async () =>
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
            string choice = string.Empty;

            


            while (choice != "5")
            {
                choice = Menu.DisplayMenu().Substring(0, 1);

                if (YourPark.Budget < -5000)
                {
                    AnsiConsole.Write(new FigletText($"YOU LOSE ASSHOLE !!! ").Centered().Color(Color.Red));
                    return;
                }
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
