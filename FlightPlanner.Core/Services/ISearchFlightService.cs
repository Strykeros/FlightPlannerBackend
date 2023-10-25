using FlightPlanner.Core.Logic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface ISearchFlightService
    {
        string From { get; set; }
        string To { get; set; }
        string DepartureDate { get; set; }
        FlightResponse GetFlight(SearchFlight inputFlight, IEntityService<Flight> service);
        Flight GetFlightById(int id, IEntityService<Flight> service);
    }
}
