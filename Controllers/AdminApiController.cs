﻿using Microsoft.AspNetCore.Authorization;
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
        private FlightStorage _flightStorage = new FlightStorage();
        private static readonly object _lockObject = new object();

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlights(int id)
        {
            return NotFound();
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

                return Ok(); 

                /*try
                {
                    _flightStorage.AddFlight(flight);

                }
                catch (BadFlightRequestException)
                {
                    return BadRequest();
                }
                catch (FlightConflictException)
                {
                    return Conflict();
                }

                return Created("", flight);*/
            }

        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlights(int id)
        {
            _flightStorage.DeleteFlight(id);
            return Ok();
        }
    }
}
