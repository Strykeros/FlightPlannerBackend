using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Core.Logic
{
    public class SearchFlight : ISearchFlightService
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
        public FlightResponse? GetFlight(SearchFlight inputFlight, IEntityService<Flight> service)
        {

            var availFlights = service.Get();

            var filteredFlights = availFlights.Where(
            flight => flight.From.AirportName == inputFlight.From &&
            flight.To.AirportName == inputFlight.To &&
            flight.DepartureTime.Contains(inputFlight.DepartureDate)
            ).ToList();

            int totalItems = filteredFlights.Count();

            var response = new FlightResponse
            {
                Items = filteredFlights,
                Page = 0,
                TotalItems = totalItems
            };

            return response;
        }

        public Flight? GetFlightById(int id, IEntityService<Flight> service)
        {
            var filteredFlight = service.GetById(id);

            if (filteredFlight == null)
                return null;

            var response = new Flight
            {
                Id = filteredFlight.Id,
                From = new Airport
                {
                    Country = filteredFlight.From.Country,
                    City = filteredFlight.From.City,
                    AirportName = filteredFlight.From.AirportName,
                },
                To = new Airport
                {
                    Country = filteredFlight.To.Country,
                    City = filteredFlight.To.City,
                    AirportName = filteredFlight.To.AirportName,
                },
                Carrier = filteredFlight.Carrier,
                DepartureTime = filteredFlight.DepartureTime,
                ArrivalTime = filteredFlight.ArrivalTime
            };

            return response;
        }
    }
}
