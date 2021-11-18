using static Crayon.Output;
using System;

namespace GUI
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
    class MenuOption<T>
    {
        public string Prompt {get; init;}
        private bool _isActive;
        public T Result {get; init;}

        /// <summary>
        /// MenuOption constructor
        /// </summary>
        /// <param name="prompt">Prompt to display in the menu</param>
        /// <param name="_delegate">Function to execute if selected</param>
        public MenuOption(string prompt, T value)
        {
            this.Prompt = prompt;
            this._isActive = false;
            this.Result = value;
        }

        /// <summary>
        /// Sets this MenuOption as the currently highlighted option
        /// </summary>
        public void SetActive()
        {
            this._isActive = true;
        }

        /// <summary>
        /// Sets this MenuOption as non-highlighted
        /// </summary>
        public void SetInactive()
        {
            this._isActive = false;
        }

        /// <summary>
        /// ToString override, manages custom prompt for being actively highlighted
        /// </summary>
        /// <returns>String representing the menuoption as it would be in the menu</returns>
        public override string ToString()
        {
            if (_isActive)
                { return $"{Bright.Blue($"❯\t{Underline(Prompt)}")}"; }

            else
                { return $"\t{Prompt}"; }
        }
    }
}
