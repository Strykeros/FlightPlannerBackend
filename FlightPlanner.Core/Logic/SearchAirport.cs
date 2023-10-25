using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Core.Logic
{
    public class SearchAirport : ISearchAirportService
    {
        public Airport? SearchForAirport(string search, IEntityService<Airport> service)
        {
            var availAirports = service.Get().
                FirstOrDefault(airport =>
                airport.AirportName.ToLower().Contains(search.ToLower().Trim()) ||
                airport.City.ToLower().Contains(search.ToLower().Trim()) ||
                airport.Country.ToLower().Contains(search.ToLower().Trim())
            );

            return availAirports;
        }
    }
}
