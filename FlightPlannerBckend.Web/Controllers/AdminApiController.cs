using FlightPlanner.Core;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerBackend.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private IEntityService<Flight> _flightService;

        public AdminApiController(IEntityService<Flight> service)
        {
            _flightService = service;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlights(int id)
        {
            Flight flight = _flightService.GetById(id);

            return flight == null ? NotFound() : Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(FlightRequest flightRequestInput)
        {
            FlightRequest flightRequest;
            Flight flight = ConvertToFlight(flightRequestInput);
            int response = _flightService.Create(flight);
            flightRequest = ConvertToFlightRequest(flight);

            if(response == 400)
            {
                return BadRequest();
            }
            if(response == 409)
            {
                return Conflict();
            }

            return Created("", flightRequest); 
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlights(int id)
        {
            _flightService.Delete(id);
            return Ok(id);
        }

        private Flight ConvertToFlight(FlightRequest flight)
        {
            return new Flight
            {
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                Carrier = flight.Carrier,
                From = new Airport
                {
                    Country = flight.From.Country,
                    City = flight.From.City,
                    AirportName = flight.From.AirportName
                },
                To = new Airport
                {
                    Country = flight.To.Country,
                    City = flight.To.City,
                    AirportName = flight.To.AirportName
                }
            };
        }

        private FlightRequest ConvertToFlightRequest(Flight flight)
        {
            return new FlightRequest
            {
                Id = flight.Id,
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                Carrier = flight.Carrier,
                From = new AirportRequest
                {
                    Country = flight.From.Country,
                    City = flight.From.City,
                    AirportName = flight.From.AirportName
                },
                To = new AirportRequest
                {
                    Country = flight.To.Country,
                    City = flight.To.City,
                    AirportName = flight.To.AirportName
                }
            };
        }
    }
}
