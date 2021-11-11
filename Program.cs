using System;

namespace Project_Backend
{
    class Program
    {
        static AirlineCoordinator aCoord;

        public static void deleteFlight()
        {
            int id;
            Console.Clear();
            Console.WriteLine(aCoord.flightList());
            Console.Write("Please enter a flight id to delete:");
            id = Convert.ToInt32(Console.ReadLine());

            if (aCoord.deleteFlight(id))
                { Console.WriteLine("Flight with id {0} deleted..", id); }
            else
                { Console.WriteLine("Flight with id {0} was not found..", id); }

            Console.WriteLine("\nPress any key to continue return to the main menu.");
            Console.ReadKey();
        }

        public static void viewFlights()
        {
            Console.Clear();
            Console.WriteLine(aCoord.flightList());
            Console.WriteLine("\nPress any key to continue return to the main menu.");
            Console.ReadKey();
        }

        public static void addFlight(){
            int flightNo,maxSeats;
            string origin, destination;

            Console.Clear();
            Console.WriteLine("-----------Add Flight----------");
            Console.Write("Please enter the flight number:");
            flightNo = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please the maximum number of seats:");
            maxSeats = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter the port of Origin:");
            origin = Console.ReadLine();
            Console.Write("Please enter the destination port:");
            destination = Console.ReadLine();

            if(aCoord.addFlight(flightNo, origin, destination, maxSeats))
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
            aCoord = new AirlineCoordinator(100, 2, 30);
            runProgram();
            Console.WriteLine("Thank you for using XYZ Airlines System. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
