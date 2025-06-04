

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
            YourPark.DisplayPark();
            EarnMoney.GenerateMoney(YourPark);
            string choice = Menu.DisplayMenu().Substring(0,1);

            


            while (choice != "5")
            {

                Console.Clear();
                AnsiConsole.Write(new FigletText(Name).Centered().Color(Color.Lime));
                YourPark.DisplayPark();
                lock (EarnMoney.GetLock())
                {
                    AnsiConsole.MarkupLine($"[green]Budget actual : {YourPark.Budget}[/]");
                    AnsiConsole.MarkupLine($"[blue]Visitors In the park : {YourPark.VisitorsEntry - YourPark.VisitorsOut}[/]");
                }
                

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
                        YourPark.PlaceSomeBuilding();
                        break;
                    case "3":
                        YourPark.RemoveSomeBuilding();
                        break;
                    case "4":
                        YourPark.BuySomeBuilding();
                        break;
                    case "5":
                        AnsiConsole.Markup("[darkred]You exit the game. Thank you ![/]");
                        break;
                    default:
                        AnsiConsole.Markup("[red]You entered a wrong input please choose a number in the menu.[/]");
                        break;
                }

                choice = Menu.DisplayMenu().Substring(0, 1);
            }

            
        }
    }
}
