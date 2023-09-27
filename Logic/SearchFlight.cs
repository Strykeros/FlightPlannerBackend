namespace FlightPlannerBackend.Logic
{
    public class SearchFlight
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }


        public FlightResponse GetFlight(SearchFlight inputFlight, FlightStorage storage)
        {
            var flights = storage.GetAllFlights();
            var filteredFlights = flights.Where(
            flight => flight.From.AirportName == inputFlight.From &&
            flight.To.AirportName == inputFlight.To &&
            flight.DepartureTime.Contains(inputFlight.DepartureDate)
            ).ToList();

            int totalItems = filteredFlights.Count;

            var response = new FlightResponse
            {
                Items = filteredFlights,
                Page = 0,
                TotalItems = totalItems
            };

            return response;
        }
    }
}
