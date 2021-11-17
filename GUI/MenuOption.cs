using static Crayon.Output;
using System;

namespace GUI.Menu
{
    /// <summary>
    /// MenuOption class
    ///
    /// PRIVATE PROPERTIES:
    ///     Prompt
    ///     IsActive
    ///     _action
    ///
    /// METHODS:
    ///     SetActive
    ///     SetInactive
    ///     Select
    ///     ToString
    /// </summary>
    class MenuOption
    {
        string Prompt;
        bool IsActive;
        Action _action;

        /// <summary>
        /// MenuOption constructor
        /// </summary>
        /// <param name="prompt">Prompt to display in the menu</param>
        /// <param name="_delegate">Function to execute if selected</param>
        public MenuOption(string prompt, Action _delegate)
        {
            this.Prompt = prompt;
            this.IsActive = false;
            this._action = _delegate;
        }

        /// <summary>
        /// Sets this MenuOption as the currently highlighted option
        /// </summary>
        public void SetActive()
        {
            this.IsActive = true;
        }

        /// <summary>
        /// Sets this MenuOption as non-highlighted
        /// </summary>
        public void SetInactive()
        {
            this.IsActive = false;
        }

        /// <summary>
        /// Selects and runs the action tied to this MenuOption
        /// </summary>
        public void Select()
        {
            this._action();
        }

        /// <summary>
        /// ToString override, manages custom prompt for being actively highlighted
        /// </summary>
        /// <returns>String representing the menuoption as it would be in the menu</returns>
        public override string ToString()
        {
            if (IsActive)
                { return $"{Bright.Blue($"❯\t{Underline(Prompt)}")}"; }

            else
                { return $"\t{Prompt}"; }
        }
    }
}
