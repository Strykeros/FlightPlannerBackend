using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerBackend.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupApiController : ControllerBase
    {
        private IDbService _dbService;

        public CleanupApiController(IDbService service)
        {
            _dbService = service;
        }


        [Route("clear")]
        [HttpPost]
        public IActionResult ClearFlights()
        {
            _dbService.DeleteAll<Flight>();
            _dbService.DeleteAll<Airport>();
            return Ok();
        }
    }
}
