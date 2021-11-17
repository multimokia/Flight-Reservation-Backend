/*
 * Michael D'mello
 * <StudentId>
 */

using System;

using Library;
using Library.Errors;

namespace Project_Backend
{
    class Program
    {
        static AirlineCoordinator _airlineCoordinator;

        public static void deleteFlight()
        {
            int id;
            Console.Clear();
            Console.WriteLine(_airlineCoordinator.FlightList());
            Console.Write("Please enter a flight id to delete:");
            id = Convert.ToInt32(Console.ReadLine());

            try
                { _airlineCoordinator.DeleteFlight(id); }

            //Catches specialized exceptions and reports their messages to the user
            catch (Exception ex) when (
                ex is FlightNotFoundException
                || ex is InvalidOperationException
            )
                { Console.WriteLine(ex.Message); }

            Console.WriteLine("\nPress any key to continue return to the main menu.");
            Console.ReadKey();
        }

        public static void viewFlights()
        {
            Console.Clear();
            Console.WriteLine(_airlineCoordinator.FlightList());
            Console.WriteLine("\nPress any key to continue return to the main menu.");
            Console.ReadKey();
        }

        public static void addFlight(){
            int flightNumber, maxSeats;
            string origin, destination;

            Console.Clear();
            Console.WriteLine("-----------Add Flight----------");
            Console.Write("Please enter the flight number:");
            flightNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please the maximum number of seats:");
            maxSeats = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter the port of Origin:");
            origin = Console.ReadLine();
            Console.Write("Please enter the destination port:");
            destination = Console.ReadLine();

            if (_airlineCoordinator.AddFlight(flightNumber, maxSeats, origin, destination))
                { Console.WriteLine("Flight successfully added.."); }
            else
                { Console.WriteLine("Flight was not added.."); }

            Console.WriteLine("\nPress any key to continue return to the main menu.");
            Console.ReadKey();
        }

        public static void showMainMenu()
        {
            Console.Clear();
            Console.WriteLine("XYZ AirLines Limited.\nPlease select a choice from the menu below:\n");
            Console.WriteLine("1: Add Flight\n2 :Add Customer\n3: View Flights\n4: View Customers");
            Console.WriteLine("5: Delete Customer\n6: Delete Flight");
            Console.WriteLine("7:Exit");
        }

        public static int getValidChoice()
        {
            int choice;
            showMainMenu();

            while (!int.TryParse(Console.ReadLine(), out choice) || (choice <1 || choice > 7))
            {
                showMainMenu();
                Console.WriteLine("Please enter a valid choice:");
            }

            return choice;
        }


        public static void runProgram()
        {
            int choice= getValidChoice();
            while (choice != 7)
            {
                if (choice == 1) { addFlight(); }
                if (choice == 2) { }
                if (choice == 3) { viewFlights(); }
                if (choice == 4) {  }
                if (choice == 5) { }
                if (choice == 6) { deleteFlight(); }
                choice = getValidChoice();
            }
        }

        static void Main(string[] args)
        {
            _airlineCoordinator = new AirlineCoordinator();
            runProgram();
            Console.WriteLine("Thank you for using XYZ Airlines System. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
