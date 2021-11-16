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

    public bool AddFlight(int flightNumber, int maxSeats, string origin, string destination)
    {
        return _flightManager.AddFlight(flightNumber, maxSeats, origin, destination);
    }

    public bool DeleteFlight(int fid)
    {
        return _flightManager.DeleteFlight(fid);
    }

    public string FlightList()
    {
        return _flightManager.GetFlightList();
    }

    public void AddCustomer(string firstName, string lastName, string phoneNumber)
    {
        _customerManager.AddCustomer(firstName, lastName, phoneNumber);
    }

    public void DeleteCustomer(string cid)
    {
        _customerManager.RemoveCustomer(cid);
    }

    public string CustomerList()
    {
        return _customerManager.GetCustomerList();
    }

    public void AddBooking(DateTime date, int flightId, string customerId)
    {
        Customer customer = _customerManager.GetCustomer(customerId);
        Flight flight = _flightManager.GetFlight(flightId);

        string bookingId = _bookingManager.AddBooking(date, flightId, customerId);
        customer.AddBookingReference(bookingId);
        flight.AddPassenger(customer);
    }

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
