using FlightPlannerBackend.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerBackend.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupApiController : ControllerBase
    {
        private FlightStorage _flightStorage;

        public CleanupApiController(FlightPlannerDbContext context)
        {
            _flightStorage = new FlightStorage(context);
        }


        [Route("clear")]
        [HttpPost]
        public IActionResult ClearFlights()
        {
            _flightStorage.ClearFlights();
            return Ok();
        }
    }
}
