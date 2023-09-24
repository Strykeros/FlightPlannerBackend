using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace FlightPlannerBackend.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private string _flightData;

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlights(int id)
        {
            return NotFound();
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult AddFlight([FromBody] Flight flight)
        {
            if (flight == null)
            {
                return BadRequest("Invalid JSON data");
            }

            _flightData = "{" +
                $"\"From airport\": \"{flight.From}\"," +
                $"\"To airport\": \"{flight.To}\"," +
                $"\"Carrier\": \"{flight.Carrier}\"," +
                $"\"Departure time\": \"{flight.DepartureTime}\"," +
                $"\"Arrival time\": \"{flight.ArrivalTime}\"," +
             "}";

            var serializedData = JsonConvert.SerializeObject(_flightData);

            return Ok(serializedData);
        }
    }
}
