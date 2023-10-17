using System.Text.Json.Serialization;

namespace FlightPlanner.Core
{
    public class AirportRequest
    {
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportName { get; set; }
    }
}
