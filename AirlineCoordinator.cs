class AirlineCoordinator
{
    FlightManager flManager;
    CustomerManager custManager;

    public AirlineCoordinator(int custIdSeed, int maxCust,int maxFlights)
    {
        flManager = new FlightManager(maxFlights);
    }

    public bool addFlight(int flightNo,string origin,string destination, int maxSeats)
    {
        return flManager.addFlight(flightNo, origin, destination, maxSeats);
    }

    public string flightList()
    {
        return flManager.getFlightList();
    }

    public bool deleteFlight(int fid)
    {
        return flManager.deleteFlight(fid);
    }
}
