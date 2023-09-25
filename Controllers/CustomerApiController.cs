using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        FlightStorage _flightStorage = new FlightStorage();

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports([FromQuery] string search)
        {
            var filteredAirports = _flightStorage.GetAllFlights()
            .FirstOrDefault(airport =>
                airport.From.AirportName.ToLower().Contains(search.ToLower().Trim()) ||
                airport.From.City.ToLower().Contains(search.ToLower().Trim()) ||
                airport.From.Country.ToLower().Contains(search.ToLower().Trim()) ||
                airport.To.AirportName.ToLower().Contains(search.ToLower().Trim()) ||
                airport.To.City.ToLower().Contains(search.ToLower().Trim()) ||
                airport.To.Country.ToLower().Contains(search.ToLower().Trim())
            );

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
        public IActionResult SearchFlights([FromBody] SearchFlight inputFlight)
        {

            if(inputFlight.From == inputFlight.To)
            {
                return BadRequest();
            }

            var filteredFlights = _flightStorage.GetAllFlights().FirstOrDefault(
                flight => flight.From.AirportName == inputFlight.From &&
                flight.To.AirportName == inputFlight.To &&
                flight.DepartureTime == inputFlight.DepartureDate
                );

            if(filteredFlights == null)
            {
                string empty = @"{ ""items"": [], ""page"": 0, ""totalItems"": 0}";
                return Ok(empty);
            }

            return Ok(filteredFlights);
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
