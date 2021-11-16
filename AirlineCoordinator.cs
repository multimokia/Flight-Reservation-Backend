class AirlineCoordinator
{
    FlightManager _flightManager;
    CustomerManager _customerManager;

    public AirlineCoordinator(int custIdSeed, int maxCust, int maxFlights)
    {
        _flightManager = new FlightManager(maxFlights);
    }

    public bool addFlight(int flightNumber, int maxSeats, string origin, string destination)
    {
        return _flightManager.AddFlight(flightNumber, maxSeats, origin, destination);
    }

    public string flightList()
    {
        return _flightManager.getFlightList();
    }

    public bool deleteFlight(int fid)
    {
        return _flightManager.DeleteFlight(fid);
    }
}
