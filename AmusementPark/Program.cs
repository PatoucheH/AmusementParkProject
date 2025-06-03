

using AmusementPark.Services;
using AmusementPark.Models;
using AmusementPark.Utils;
using Spectre.Console;
using System.Threading.Tasks;

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
            AnsiConsole.Write(new FigletText(Name).Centered().Color(Color.Lime));

            string choice = Menu.DisplayMenu().Substring(0,1);
            int visitors = 0;
            _ = Task.Run(async () =>
            { 
                while (true)
                {
                    visitors = EarnMoney.CalculateNumberVisitor();
                    double moneyEarned = EarnMoney.EarnMoneyByVisitorEntry(visitors, YourPark);
                    YourPark.Budget += moneyEarned;
                    if(moneyEarned > 0) AnsiConsole.MarkupLine($"[green]You just earned {moneyEarned}[/] euros" +
                        $"\n Your budget is now : {YourPark.Budget}");

                    await Task.Delay(30_000); 
                }
            });
            

            while (choice != "7")
            {
                if (YourPark.Budget < -5000) AnsiConsole.MarkupLine($"[red]YOU LOSE ASSHOLE !!! [/]");
                switch (choice)
                {
                    case "1":
                        YourPark.DisplayPark();
                        break;
                    case "2":
                        YourPark.DisplayInventory();
                        break;
                    case "3":
                        YourPark.PlaceSomeBuilding();
                        break;
                    case "4":
                        YourPark.RemoveSomeBuilding();
                        break;
                    case "5":
                        YourPark.BuySomeBuilding();
                        break;
                    case "7":
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
