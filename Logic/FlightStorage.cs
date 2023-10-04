using Microsoft.EntityFrameworkCore;

namespace FlightPlannerBackend.Logic
{
    public class FlightStorage
    {
        private readonly FlightPlannerDbContext _context;

        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public int AddFlight(Flight flight)
        {
            return InsertFlight(flight);
        }

        private int InsertFlight(Flight flight)
        {
            bool flightIsNull = string.IsNullOrEmpty(flight.ArrivalTime) ||
                                string.IsNullOrEmpty(flight.DepartureTime) ||
                                string.IsNullOrEmpty(flight.Carrier) ||
                                flight.From == null ||
                                 string.IsNullOrEmpty(flight.From.Country) ||
                                 string.IsNullOrEmpty(flight.From.City) ||
                                 string.IsNullOrEmpty(flight.From.AirportName) ||
                                flight.To == null ||
                                 string.IsNullOrEmpty(flight.To.Country) ||
                                 string.IsNullOrEmpty(flight.To.City) ||
                                 string.IsNullOrEmpty(flight.To.AirportName);

            bool flightExists = _context.Flights.Any(existingFlight =>
                existingFlight.From.AirportName == flight.From.AirportName &&
                existingFlight.From.City == flight.From.City &&
                existingFlight.From.Country == flight.From.Country &&
                existingFlight.To.AirportName == flight.To.AirportName &&
                existingFlight.To.City == flight.To.City &&
                existingFlight.To.Country == flight.To.Country &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime &&
                existingFlight.To.AirportName == flight.To.AirportName
            );

            bool dateMismatch = DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime);

            if (flight.From.AirportName.ToLower().Trim() == flight.To.AirportName.ToLower().Trim() || flightIsNull || dateMismatch)
            {
                return 400;
            }

            if (flightExists)
            {
                return 409;
            }

            _context.Flights.Add(flight);
            _context.SaveChanges();
            return 201;
        }

        public Flight GetFlight(int id)
        {
            return _context.Flights.Include(f => f.From).Include(f => f.To).SingleOrDefault(f => f.Id == id);
        }

        public List<Flight> GetAllFlights()
        {
            return _context.Flights.Include(f => f.From).Include(f => f.To).ToList();
        }

        public void ClearFlights()
        {
            if(_context.Flights.Any())
            {
                _context.Flights.RemoveRange(_context.Flights);
                _context.SaveChanges();
            }
        }

        public void DeleteFlight(int id)
        {
            var flight = _context.Flights.Include(f => f.From).Include(f => f.To).SingleOrDefault(f => f.Id == id);

            if(flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }
        }

        public Flight SearchAirport(string search)
        {
            var foundAirports = GetAllFlights()
            .FirstOrDefault(airport =>
                airport.From.AirportName.ToLower().Contains(search.ToLower().Trim()) ||
                airport.From.City.ToLower().Contains(search.ToLower().Trim()) ||
                airport.From.Country.ToLower().Contains(search.ToLower().Trim()) ||
                airport.To.AirportName.ToLower().Contains(search.ToLower().Trim()) ||
                airport.To.City.ToLower().Contains(search.ToLower().Trim()) ||
                airport.To.Country.ToLower().Contains(search.ToLower().Trim())
            );

            return foundAirports;
        }
    }
}
