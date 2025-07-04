﻿using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Utils
{
    internal class Menu
    {
        /// <summary>
        /// A method with the prompt to display to the user to let him choice what to do 
        /// </summary>
        /// <returns>The menu to display to the user</returns>
        public static string DisplayMenu()
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[lime]What do you want to do in your park ?[/] ")
                .AddChoices(new[]
                {
                    "1.View your inventory",
                    "2.Place anything on your park",
                    "3.Remove a building from your park",
                    "4.Buy some new building for your park",
                    "5.Save the game",
                    "6.Show visitors by attractions",
                    "7.Exit",
                }));
        }

        public static string NewOrSaveGame()
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(new[]
                {
                    "1.New game",
                    "2.Load a game",
                }));

        }
    }
}
