using FlightPlannerBackend.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerBackend.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private FlightStorage _flightStorage;
        private static readonly object _lockObject = new object();
        private readonly FlightPlannerDbContext _context;

        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
            _flightStorage = new FlightStorage(context);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlights(int id)
        {
            lock (_lockObject)
            { 
                var flight = _flightStorage.GetFlight(id);

                if (flight == null)
                {
                    return NotFound();
                }

                return Ok(flight);            
            }
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_lockObject)
            {
                int flightAdded = _flightStorage.AddFlight(flight);

                if (flightAdded == 201)
                {
                    return Created("flight added", _flightStorage.GetFlight(flight.Id)); 
                }
                else if (flightAdded == 409)
                {
                    return Conflict();
                }
                else if (flightAdded == 400)
                {
                    return BadRequest();
                }

                return Ok(flightAdded); 
            }

        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlights(int id)
        {
            lock (_lockObject)
            {
                _flightStorage.DeleteFlight(id);
                return Ok(id);
            }
        }
    }
}
