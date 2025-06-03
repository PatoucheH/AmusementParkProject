using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Utils
{
    internal class Menu
    {
        public static string DisplayMenu()
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[lime]What do you want to do in your park ?[/] ")
                .AddChoices(new[]
                {
                    "1.Display All your park",
                    "2.View your inventory",
                    "3.Place anything on your park",
                    "4.Remove a building from your park",
                    "5.Buy some new building for your park",
                    "7.Exit"
                }));
        }
    }
}
