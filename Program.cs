/*
 * Michael D'mello
 * <StudentId>
 */

using System;
using System.Collections.Generic;

using Library;
using static Crayon.Output;
using static GUI.InterfaceUtils;
using GUI;

namespace Project_Backend
{
    class Program
    {
        static AirlineCoordinator _airlineCoordinator;

        static void Main(string[] args)
        {
            _airlineCoordinator = new AirlineCoordinator();
            MainMenu();
            Success("Thank you for using XYZ Airlines System. Press any key to exit.");
        }

        public static void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                MenuOption<Action> choice = Menu(
                    prompt: "What would you like to manage?",
                    new MenuOption<Action>("Customers", CustomerMenu),
                    new MenuOption<Action>("Flights", FlightMenu),
                    new MenuOption<Action>("Bookings", BookingMenu),
                    new MenuOption<Action>("Exit", () => { exit = true; })
                );

                choice.Result();
            }
        }

        #region Customer Menu
        public static void CustomerMenu()
        {
            bool done = false;
            while (!done)
            {
                MenuOption<Action> choice = Menu(
                    prompt: "What would you like to do?",
                    new MenuOption<Action>("Add a customer", AddFlight),
                    new MenuOption<Action>("View all customers", ViewAllFlights),
                    new MenuOption<Action>("Delete a customer", DeleteFlight),
                    new MenuOption<Action>("Back", () => { done = true; })
                );

                choice.Result();
            }
        }
        #endregion

        #region Booking Menu
        public static void BookingMenu()
        {
            bool done = false;
            while (!done)
            {
                MenuOption<Action> choice = Menu(
                    prompt: "Please select a choice from the menu below",
                    new MenuOption<Action>("Make a booking", AddBooking),
                    new MenuOption<Action>("View all bookings", ViewAllFlights),
                    new MenuOption<Action>("Delete a booking", DeleteFlight),
                    new MenuOption<Action>("Back", () => { done = true; })
                );

                choice.Result();
            }
        }

        public static void AddBooking()
        {
            Customer[] customers = _airlineCoordinator.GetCustomers();
            Flight[] flights = _airlineCoordinator.GetFlights();

            //There must be a customer and/or flight to make a booking
            if (customers.Length == 0)
            {
                Error("You must have at least one customer to make a booking.");
                return;
            }

            if (flights.Length == 0)
            {
                Error("You must have at least one flight to make a booking.");
                return;
            }

            //Checks pass, let's allow the user to select a customer and flight
            Customer chosenCustomer = Select<Customer>("Which customer would you like to make a booking for?", customers);
            Flight chosenFlight = Select<Flight>("Which flight would you like to book them on?", flights);

            //Now we'll get info to create a datetime
            //Prompt for year first so we can see if it's a leap year
            int yearOfBirth = PromptInt("Please enter the person's birth year", x => x <= DateTime.Now.Year);
            //Now prompt for month so we know how many days
            int monthOfBirth = PromptInt("Please enter the person's month of birth", x => x > 0 && x < 13);
            //Now handle getting the year
            int dayOfBirth = PromptInt(
                "Please enter the person's day of birth",
                x => x > 0 && x <= GetAmountOfDaysInMonth(monthOfBirth, yearOfBirth % 4 == 0)
            );

            if (
                _airlineCoordinator.AddBooking(new DateTime(yearOfBirth, monthOfBirth, dayOfBirth), chosenFlight, chosenCustomer)
            )
                { Success("Booking added successfully."); }
            else
                { Error("A booking with that specification already exists."); }
        }
        #endregion

        #region Flight Menu

        /// <summary>
        /// Flight menu entrypoint
        /// </summary>
        public static void FlightMenu()
        {
            bool done = false;
            while (!done)
            {
                MenuOption<Action> choice = Menu(
                    prompt: "What would you like to do?",
                    new MenuOption<Action>("Add a flight", AddFlight),
                    new MenuOption<Action>("View all flights", ViewAllFlights),
                    new MenuOption<Action>("View a specific flight", ViewSpecificFlight),
                    new MenuOption<Action>("Delete a flight", DeleteFlight),
                    new MenuOption<Action>("Back", () => { done = true; })
                );

                choice.Result();
            }
        }

        /// <summary>
        /// Menu flow for adding flights
        /// </summary>
        public static void AddFlight()
        {
            int flightNumber, maxSeats;
            string origin, destination;

            flightNumber = PromptInt("Please enter a flight number");
            maxSeats = PromptInt("Please enter the maximum number of seats");
            origin = Prompt("Please enter the origin airport");
            destination = Prompt("Please enter the destination airport");

            if (_airlineCoordinator.AddFlight(flightNumber, maxSeats, origin, destination))
                { Success("Flight successfully added!"); }
            else
                { Error($"Flight was not added, the flight number {flightNumber} is already registered."); }
        }

        /// <summary>
        /// Menu flow for viewing all flights
        /// </summary>
        public static void ViewAllFlights()
        {
            Flight[] flights = _airlineCoordinator.GetFlights();

            if (flights.Length == 0)
            {
                Error("There are no flights to display.");
                return;
            }

            foreach (Flight flight in flights)
                { Console.WriteLine($"{Bright.Yellow(flight.FlightNumber.ToString())}: {flight.OriginAirport} -> {flight.DestinationAirport}"); }

            PromptToContinue();
        }

        /// <summary>
        /// Menu flow for viewing a specific flight
        /// </summary>
        public static void ViewSpecificFlight()
        {
            Flight choice = Select<Flight>("Which flight would you like to view?", _airlineCoordinator.GetFlights());

            //User opted to go back or there's no flights
            if (choice == null)
                { return; }

            Success(choice.ToString());
        }

        /// <summary>
        /// Menu flow for deleting a flights
        /// </summary>
        public static void DeleteFlight()
        {
            Flight choice = Select<Flight>("Which flight would you like to delete?", _airlineCoordinator.GetFlights());

            //User opted to go back or there's no flights
            if (choice == null)
                { return; }

            //The delete method will throw errors if the flgiht doesn't exist or it has bookings. We try/catch to raise those to the user.
            try
            {
                _airlineCoordinator.DeleteFlight(choice.FlightNumber);
                Success("Flight deleted.");
            }

            //Catches specialized exceptions and reports their messages to the user
            catch (InvalidOperationException ex)
                { Error(ex.Message); }
        }
        #endregion

        /// <summary>
        /// Utility method, gets the number of days in the month provided (accounts for leap years)
        /// </summary>
        /// <param name="month">month to get the amount of days for</param>
        /// <param name="isLeapYear">whether or not it's a leap year</param>
        /// <returns>Amount of days in month</returns>
        static int GetAmountOfDaysInMonth(int month, bool isLeapYear)
        {
            int AmtDays = 0;
            switch (month)
            {
                case 1:
                    AmtDays = 31;
                    break;
                case 2:
                    AmtDays = 28;
                    if (isLeapYear)
                        AmtDays++;
                    break;
                case 3:
                    AmtDays = 31;
                    break;
                case 4:
                    AmtDays = 30;
                    break;
                case 5:
                    AmtDays = 31;
                    break;
                case 6:
                    AmtDays = 30;
                    break;
                case 7:
                    AmtDays = 31;
                    break;
                case 8:
                    AmtDays = 31;
                    break;
                case 9:
                    AmtDays = 30;
                    break;
                case 10:
                    AmtDays = 31;
                    break;
                case 11:
                    AmtDays = 30;
                    break;
                case 12:
                    AmtDays = 31;
                    break;
            }

            return AmtDays;
        }
    }
}
