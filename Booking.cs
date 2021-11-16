using System;

public class Booking
{
    /// <summary>
    /// Id of the booking
    /// </summary>
    private string _id;
    public string Id {get { return _id; }}

    /// <summary>
    /// Id of the flight associated with the booking
    /// </summary>
    private int _flightId;
    public int FlightId {get { return _flightId; }}

    /// <summary>
    /// Id of the customer associated with the booking
    /// </summary>
    private string _customerId;
    public string CustomerId {get { return _customerId; }}

    /// <summary>
    /// Date of the booking as timestamp
    /// </summary>
    private long _date;
    public long Date {get { return _date; }}

    public Booking(DateTime date, int flightId, string customerId)
    {
        _date = date.ToTimestamp();
        _flightId = flightId;
        _customerId = customerId;

        // Generate a unique id
        _id = Utilities.HashString($"{flightId}{customerId}{_date}");
    }

    public DateTime GetBookingDateTime()
    {
        return Utilities.FromTimestamp(_date);
    }

    /// <summary>
    /// ToString override
    /// </summary>
    /// <returns>String representing the booking</returns>
    public override string ToString()
    {
        return $"{Id} {GetBookingDateTime()} {FlightId} {CustomerId}";
    }
}
