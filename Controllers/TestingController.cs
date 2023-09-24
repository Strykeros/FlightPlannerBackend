using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerBackend.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private FlightStorage _flightStorage = new FlightStorage();
        [Route("clear")]
        [HttpPost]
        public IActionResult ClearFlights()
        {
            _flightStorage.ClearFlights();
            return Ok();
        }
    }
}
