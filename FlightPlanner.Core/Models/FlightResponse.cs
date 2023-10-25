using FlightPlanner.Core.Models;

namespace FlightPlanner.Core
{
    public class FlightResponse
    {
        public List<Flight> Items { get; set; }
        public int Page { get; set; }
        public int TotalItems { get; set; }
    }
}
