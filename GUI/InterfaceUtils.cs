using System;
using System.Collections.Generic;
using static Crayon.Output;

namespace GUI
{
    /// <summary>
    /// Static class for console UI utilities
    /// </summary>
    static class InterfaceUtils
    {
        /// <summary>
        /// Menu creator
        /// </summary>
        /// <param name="prompt">The main prompt for the menu</param>
        /// <param name="menuItems">Array of MenuOptions representing the options of the menu</param>
        /// <returns>MenuOption representing the selected option</returns>
        public static MenuOption<T> Menu<T>(string prompt, params MenuOption<T>[] menuItems)
        {
            int currItem = 0;
            ConsoleKeyInfo key;

            do
            {
                //Clear the console so we're not stacking menu upon menu
                Console.Clear();

                //Prompt
                Console.Write(Bright.Blue("? ") + Bold(prompt));
                Console.WriteLine(Bold(Bright.Black("› Hint - Use the arrow keys to move and hit enter to submit.")));

                //Loop
                for (int i=0; i<menuItems.Length; i++)
                {

                    //Set the current item as active
                    if (currItem == i)
                    {
                        menuItems[i].SetActive();
                        Console.WriteLine(menuItems[i]);
                    }

                    //Ensure all others are inactive
                    else
                    {
                        menuItems[i].SetInactive();
                        Console.WriteLine(menuItems[i]);
                    }
                }

                key = Console.ReadKey(true);

                //Downarrow will increase the counter (which moves the active selection down)
                if (key.Key.ToString() == "DownArrow")
                {
                    currItem++;

                    //Loop currItem back
                    if (currItem > menuItems.Length - 1)
                        { currItem = 0; }
                }

                //Uparrow decreases it and moves the active selection up
                else if (key.Key.ToString() == "UpArrow")
                {
                    currItem--;

                    //Loop cirrItem back around
                    if (currItem < 0)
                        { currItem = Convert.ToInt16(menuItems.Length - 1); }
                }

            } while (key.KeyChar != 13); // Loop around until the user presses the enter enter.

            //Clear the console on exit as we should be moving to a new screen here
            Console.Clear();

            //Finally return the selected item
            return menuItems[currItem];
        }

        /// <summary>
        /// Generic prompt screen, allows for custom validation
        /// </summary>
        /// <param name="prompt">Prompt to display to the user</param>
        /// <param name="predicate">Validation function</param>
        /// <returns></returns>
        public static string Prompt(string prompt, Func<string, bool> predicate)
        {
            string userValue = "";

            do
            {
                Console.Clear();
                Console.Write($"{Bold(prompt)}\n\n{Bright.Red("❯  ")}");
                userValue = Console.ReadLine();
            } while (!predicate(userValue));

            Console.Clear();
            return userValue;
        }

        public static int PromptInt(string prompt)
        {
            return Convert.ToInt32(Prompt(prompt, (s) => { return int.TryParse(s, out int _); }));
        }

        /// <summary>
        /// Internal function for number prompting
        /// </summary>
        /// <param name="prompt">Text to prompt the user with</param>
        /// <param name="predicate">Function for validation</param>
        /// <returns>integer representing the number prompted for</returns>
        public static int PromptInt(string prompt, Func<int, bool> predicate)
        {
            int rv = -1;
            do
            {
                bool parsed = int.TryParse(Prompt(prompt), out rv) && predicate(rv);
                if (!parsed)
                    { Error("Please enter a valid number"); }

            } while (!predicate(rv));

            return rv;
        }

        /// <summary>
        /// Overload for Prompt, specific to ensure non-empty strings (can be toggled)
        /// </summary>
        /// <param name="prompt">Text to prompt the user with</param>
        /// <param name="allowEmpty">whether or not an empty string is acceptable input</param>
        /// <returns>The result the user entered</returns>
        public static string Prompt(string prompt, bool allowEmpty=false)
        {
            return Prompt(prompt, x => allowEmpty || x.Trim() != "");
        }

        /// <summary>
        /// Generic error screen, displays a message to the user
        /// </summary>
        /// <param name="message">Error message to display</param>
        public static void Error(string message)
        {
            Console.Write($"{Bold(Bright.Red($"⨯ {message}"))}");
            Console.ReadLine();
        }

        /// <summary>
        /// Error screen allowing a callback function to be used for follow up actions
        /// </summary>
        /// <param name="message">Error message to display</param>
        /// <param name="callback">Callback function to execute</param>
        public static void Error(string message, Action callback)
        {
            Console.Write($"{Bold(Bright.Red($"⨯ {message}"))}");
            Console.ReadLine();
            callback();
        }

        /// <summary>
        /// Generic success message screen
        /// </summary>
        /// <param name="message">Message to display</param>
        public static void Success(string message)
        {
            Console.Write($"{Bold(Green($"🗸 {message}"))}");
            Console.ReadLine();
        }

        /// <summary>
        /// Success screen with callback
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="callback">Callback function to execute</param>
        public static void Success(string message, Action callback)
        {
            Console.Write($"{Bold(Green($"🗸 {message}"))}");
            Console.ReadLine();
            callback();
        }

        /// <summary>
        /// Simple press enter to continue screen
        /// </summary>
        public static void PromptToContinue()
        {
            Console.WriteLine(Bold("Press enter to continue..."));
            Console.ReadKey();
        }

        /// <summary>
        /// Helper method to select a specific entry of any MenuOptionValue type from an array of them
        /// </summary>
        /// <param name="prompt">Prompt to use for the menu</param>
        /// <param name="rawOptions"></param>
        /// <typeparam name="T">A child of MenuOptionValue</typeparam>
        /// <returns>Object of type T that was selected</returns>
        public static T Select<T>(string prompt, T[] rawOptions) where T : MenuOptionValue
        {
            //Nothing to delete, show an error and return
            if (rawOptions.Length == 0)
            {
                Error($"There are no {typeof(T).Name.ToLower()}s registered.");
                return null;
            }

            List<MenuOption<T>> options = new List<MenuOption<T>>();

            foreach (T rawOption in rawOptions)
            {
                options.Add(
                    new MenuOption<T>(
                        rawOption.GetMenuPrompt(),
                        rawOption
                    )
                );
            }

            options.Add(
                new MenuOption<T>("Back", null)
            );

            return Menu(prompt, options.ToArray()).Result;
        }
    }
}
