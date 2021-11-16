using System;
using System.Collections.Generic;

class BookingManager
{
    private Dictionary<string, Booking> _bookings;

    public BookingManager()
    {
        this._bookings = new Dictionary<string, Booking>();
    }

    /// <summary>
    /// Creates a new booking.
    /// </summary>
    /// <param name="date">DateTime of the booking</param>
    /// <param name="flightId">Id of the associated flight</param>
    /// <param name="customerId">Id of the associated customer</param>
    /// <exception cref="DuplicateBookingException">Thrown when a booking already exists for the given flight and customer</exception>
    /// <returns>Id of the created booking</returns>
    public string AddBooking(DateTime date, int flightId, string customerId)
    {
        Booking booking = new Booking(date, flightId, customerId);

        if (this._bookings.ContainsKey(booking.Id))
            { throw new DuplicateBookingException(booking); }

        //Add the booking to the dict and register its reference in the customer
        this._bookings.Add(booking.Id, booking);
        return booking.Id;
    }

    /// <summary>
    /// Deletes a booking
    /// </summary>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    public bool RemoveBooking(string bookingId)
    {
        if (!this._bookings.ContainsKey(bookingId))
            { return false; }

        return this._bookings.Remove(bookingId);
    }

    public Booking GetBooking(string bookingId)
    {
        if (!this._bookings.ContainsKey(bookingId))
            { return null; }

        return this._bookings[bookingId];
    }
}
