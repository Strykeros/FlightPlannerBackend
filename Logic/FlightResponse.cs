namespace FlightPlannerBackend.Logic
{
    public class FlightResponse
    {
        public List<Flight> Items { get; set; }
        public int Page { get; set; }
        public int TotalItems { get; set; }
    }
}
