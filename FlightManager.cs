using System;
using System.Collections.Generic;

class FlightManager
{
    private Dictionary<int, Flight> _flights;

    public FlightManager()
    {
        _flights = new Dictionary<int, Flight>();
    }

    public bool AddFlight(int flightNumber, int maxSeats, string origin, string destination)
    {
        //If we already have this flight, don't try to add again
        if (HasFlight(flightNumber))
            { return false; }

        Flight flight = new Flight(flightNumber, maxSeats, origin, destination);
        _flights.Add(flightNumber, flight);
        return true;
    }

    public bool HasFlight(int flightNumber)
    {
        return _flights.ContainsKey(flightNumber);
    }

    public Flight GetFlight(int flightNumber)
    {
        Flight flight = null;
        _flights.TryGetValue(flightNumber, out flight);
        return flight;
    }

    public bool DeleteFlight(int flightNumber)
    {
        //Flights can only be deleted if they have no passengers
        Flight flight = GetFlight(flightNumber);

        if (flight == null)
            { throw new FlightNotFoundException(flightNumber); }

        if (flight.GetNumPassengers() > 0)
            { throw new InvalidOperationException("Flights may only be deleted if they have no passengers booked."); }

        //If checks pass, remove the flight and return status
        return _flights.Remove(flightNumber);
    }

    public string GetFlightList()
    {
        string rv = "Flight List:";
        foreach (KeyValuePair<int, Flight> flight in _flights)
            { rv += $"\n\t{flight.Key} from {flight.Value.OriginAirport} to {flight.Value.DestinationAirport}"; }
        return rv;
    }
}
