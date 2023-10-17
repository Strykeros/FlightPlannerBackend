using FlightPlanner.Core.Models;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Logic;
using FlightPlanner.Core.Services;

namespace FlightPlannerBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private IEntityService<Airport> _airportEntityService;
        private IEntityService<Flight> _flightEntityService;
        private SearchAirport _airport;
        private SearchFlight _flight;

        public CustomerApiController(IEntityService<Airport> service, IEntityService<Flight> flightEntityService)
        {
            _airportEntityService = service;
            _airport = new SearchAirport();
            _flight = new SearchFlight();
            _flightEntityService = flightEntityService;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports([FromQuery] string search)
        {
            var filteredAirports = _airport.SearchForAirport(search, _airportEntityService);
            return filteredAirports != null ? Ok(new List<Airport>() { filteredAirports }) : NotFound();
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlight inputFlight)
        {
            if (inputFlight.From == inputFlight.To)
                return BadRequest();

            var response = _flight.GetFlight(inputFlight, _flightEntityService);

            return Ok(response);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlightById(int id)
        {
            var flight = _flight.GetFlightById(id, _flightEntityService);

            return flight == null ? NotFound() : Ok(flight);
        }
    }
}
