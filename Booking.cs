class Booking
{
    private int _id;
    public int Id {get { return _id; }}
    private int _flightId;
    public int FlightId {get { return _flightId; }}
    private string _customerId;
    public string CustomerId {get { return _customerId; }}

    public Booking(int id, int flightId, string customerId)
    {
        _id = id;
        _flightId = flightId;
        _customerId = customerId;
    }
}
