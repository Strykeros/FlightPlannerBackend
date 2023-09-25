namespace FlightPlannerBackend
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;
        public int AddFlight(Flight flight)
        {
            bool flightExists = _flights.Exists(existingFlight =>
                existingFlight.From.AirportName == flight.From.AirportName &&
                existingFlight.From.City == flight.From.City &&
                existingFlight.From.Country == flight.From.Country &&
                existingFlight.To.AirportName == flight.To.AirportName &&
                existingFlight.To.City == flight.To.City &&
                existingFlight.To.Country == flight.To.Country &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime
            );

            bool flightIsNull = string.IsNullOrEmpty(flight.ArrivalTime) ||
                                string.IsNullOrEmpty(flight.DepartureTime) ||
                                string.IsNullOrEmpty(flight.Carrier) ||
                                (flight.From == null ||
                                 string.IsNullOrEmpty(flight.From.Country) ||
                                 string.IsNullOrEmpty(flight.From.City) ||
                                 string.IsNullOrEmpty(flight.From.AirportName)) ||
                                (flight.To == null ||
                                 string.IsNullOrEmpty(flight.To.Country) ||
                                 string.IsNullOrEmpty(flight.To.City) ||
                                 string.IsNullOrEmpty(flight.To.AirportName));

            bool dateMismatch = DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime);

            if (flight.From.AirportName.ToLower().Trim() == flight.To.AirportName.ToLower().Trim() || flightIsNull || dateMismatch)
            {
                return 400;
            }

            if (flightExists)
            {
                return 409;
            }

            flight.Id = _id++;
            _flights.Add(flight);
            return 201;    
        }

        public Flight GetFlight(int id)
        {
            var flight = _flights.FirstOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return null;
            }

            return flight;
        }

        public List<Flight> GetAllFlights()
        {
            return _flights;
        }

        public void ClearFlights()
        {
            _flights.Clear();
        }

        public void DeleteFlight(int id)
        {
            var flight = _flights.FirstOrDefault(f => f.Id == id);
            _flights.Remove(flight);
        }
    }
}
