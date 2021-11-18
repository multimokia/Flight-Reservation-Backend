using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    class AirlineCoordinator
    {
        FlightManager _flightManager;
        CustomerManager _customerManager;
        BookingManager _bookingManager;

        public AirlineCoordinator()
        {
            _flightManager = new FlightManager();
            _customerManager = new CustomerManager();
            _bookingManager = new BookingManager();
        }

        /// <summary>
        /// Adds a flight to the flight manager.
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <param name="maxSeats"></param>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool AddFlight(int flightNumber, int maxSeats, string origin, string destination)
        {
            return _flightManager.AddFlight(flightNumber, maxSeats, origin, destination);
        }

        /// <summary>
        /// Deletes a flight from the flight manager.
        /// </summary>
        /// <param name="flightId">Id of the flight to delete</param>
        /// <exception cref="FlightNotFoundException">Thrown if the flight is not found</exception>
        /// <exception cref="InvalidOperationException">Thrown if the flight has bookings</exception>
        public void DeleteFlight(int flightId)
        {
            _flightManager.DeleteFlight(flightId);
        }

        /// <summary>
        /// Gets an array of all flights.
        /// </summary>
        /// <returns>Array of Flights</returns>
        public Flight[] GetFlights()
        {
            return _flightManager.GetAllFlights().Values.ToArray();
        }

        /// <summary>
        /// Adds a customer to the customer manager.
        /// </summary>
        /// <param name="firstName">Customer's first name</param>
        /// <param name="lastName">Customer's last name</param>
        /// <param name="phoneNumber">Customer's phone number</param>
        public void AddCustomer(string firstName, string lastName, string phoneNumber)
        {
            _customerManager.AddCustomer(firstName, lastName, phoneNumber);
        }

        /// <summary>
        /// Removes a customer from the customer manager.
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        /// <exception cref="CustomerNotFoundException">If the customer does not exist</exception>
        /// <exception cref="InvalidOperationException">If the customer cannot be deleted because they have bookings</exception>
        public void DeleteCustomer(string customerId)
        {
            _customerManager.RemoveCustomer(customerId);
        }

        /// <summary>
        /// Gets an array of all registered customers.
        /// </summary>
        /// <returns>Array of customers</returns>
        public Customer[] GetCustomers()
        {
            return _customerManager.GetCustomers().Values.ToArray();
        }

        /// <summary>
        /// Adds a booking to the booking manager.
        /// </summary>
        /// <param name="date">Date and Time of the bookingt</param>
        /// <param name="flight">Associated Flight</param>
        /// <param name="customer">Associated Customer</param>
        /// <returns>true if the booking was added, false otherwise</returns>
        public bool AddBooking(DateTime date, Flight flight, Customer customer)
        {
            try
            {
                string bookingId = _bookingManager.AddBooking(date, flight.FlightNumber, customer.Id);
                customer.AddBookingReference(bookingId);
                flight.AddPassenger(customer);
            }

            catch (Errors.DuplicateBookingException)
                { return false; }

            return true;
        }

        /// <summary>
        /// Delete a booking from the booking manager.
        /// </summary>
        /// <param name="bookingId">Id of the booking to delete</param>
        public void DeleteBooking(string bookingId)
        {
            Booking booking = _bookingManager.GetBooking(bookingId);
            Customer customer = _customerManager.GetCustomer(booking.CustomerId);
            Flight flight = _flightManager.GetFlight(booking.FlightId);

            _bookingManager.RemoveBooking(bookingId);
            customer.RemoveBookingReference(bookingId);
            flight.RemovePassenger(booking.CustomerId);
        }
    }
}
