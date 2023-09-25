using System.Diagnostics.Contracts;

namespace FlightPlannerBackend
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static Thread _addFlightThread;
        private static Thread _getFlightThread;
        private static readonly object _lockObject = new object();
        private static int _id = 1;

        public int AddFlight(Flight flight)
        {
            return InitAddFlightThread(flight);
        }

        private int InitAddFlightThread(Flight flight)
        {
            int output = 0;
            _addFlightThread = new Thread((() =>
            {
                output = InsertFlight(flight);
            }));
            _addFlightThread.Start();
            return output;
        }

        private int InsertFlight(Flight flight)
        {
            bool flightExists = _flights.Exists(existingFlight =>
                existingFlight.From.AirportName == flight.From.AirportName &&/*
                existingFlight.From.City == flight.From.City &&
                existingFlight.From.Country == flight.From.Country &&
                existingFlight.To.AirportName == flight.To.AirportName &&
                existingFlight.To.City == flight.To.City &&
                existingFlight.To.Country == flight.To.Country &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime &&*/
                existingFlight.To.AirportName == flight.To.AirportName
            );

            bool flightIsNull = string.IsNullOrEmpty(flight.ArrivalTime) &&
                                string.IsNullOrEmpty(flight.DepartureTime) &&
                                string.IsNullOrEmpty(flight.Carrier) &&
                                //flight.From == null ||
                                 string.IsNullOrEmpty(flight.From.Country) &&
                                 string.IsNullOrEmpty(flight.From.City) &&
                                 string.IsNullOrEmpty(flight.From.AirportName) &&
                                //flight.To == null ||
                                 string.IsNullOrEmpty(flight.To.Country) &&
                                 string.IsNullOrEmpty(flight.To.City) &&
                                 string.IsNullOrEmpty(flight.To.AirportName);

            bool dateMismatch = DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime);

            if (flightIsNull || dateMismatch || flight.From == null || flight.To == null)
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

        private Flight InitGetFlightThread(int id)
        {
            Flight output = null;
            _getFlightThread = new Thread((() =>
            {
                output = ObtainFlight(id);
            }));
            _getFlightThread.Start();
            return output;
        }

        private Flight ObtainFlight(int id)
        {
            var flight = _flights.FirstOrDefault(f => f.Id == id);

            return flight;                
            
        }

        public Flight GetFlight(int id)
        {
            return InitGetFlightThread(id);
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

        public bool ContainsTime(string dateStr)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateStr, out dateTime))
                return dateTime.TimeOfDay != TimeSpan.Zero;
            return false;
        }
    }
}
