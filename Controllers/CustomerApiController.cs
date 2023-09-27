using FlightPlannerBackend.Logic;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        FlightStorage _flightStorage = new FlightStorage();
        FlightResponse _flightResponse = new FlightResponse();
        SearchFlight _searchFlight = new SearchFlight();

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports([FromQuery] string search)
        {
            var filteredAirports = _flightStorage.SearchAirport(search);

            if (filteredAirports != null)
            {
                var airportList = new List<Airport>() { filteredAirports.From };
                return Ok(airportList);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlight inputFlight)
        {
            if (inputFlight.From == inputFlight.To)
            {
                return BadRequest();
            }

            var response = _searchFlight.GetFlight(inputFlight, _flightStorage);

            return Ok(response);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlightById(int id)
        {
            var flight = _flightStorage.GetFlight(id);

            if(flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
