using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerBackend.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {

        [Route("clear")]
        [HttpPost]
        public IActionResult ClearFlights()
        {
            return Ok();
        }
    }
}
