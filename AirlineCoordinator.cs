using System;

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
    /// Lists all flights in the flight manager.
    /// </summary>
    /// <returns>String representing all flights stored in the flight manager</returns>
    public string FlightList()
    {
        return _flightManager.GetFlightList();
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
    /// Lists out all customers in the customer manager in a human readable format.
    /// </summary>
    /// <returns>human readable list of customer + info</returns>
    public string CustomerList()
    {
        return _customerManager.GetCustomerList();
    }

    /// <summary>
    /// Adds a booking to the booking manager.
    /// </summary>
    /// <param name="date">Date and Time of the bookingt</param>
    /// <param name="flightId">Id of the associated flight</param>
    /// <param name="customerId">Id of the associated customer</param>
    /// <exception cref="DuplicateBookingException">If a booking with the given info already exists</exception>
    public void AddBooking(DateTime date, int flightId, string customerId)
    {
        Customer customer = _customerManager.GetCustomer(customerId);
        Flight flight = _flightManager.GetFlight(flightId);

        string bookingId = _bookingManager.AddBooking(date, flightId, customerId);
        customer.AddBookingReference(bookingId);
        flight.AddPassenger(customer);
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
