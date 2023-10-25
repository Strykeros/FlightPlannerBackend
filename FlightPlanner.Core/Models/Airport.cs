
using System.Text.Json.Serialization;

namespace FlightPlanner.Core.Models
{
    public class Airport : Entity
    {
        [JsonIgnore]
        public override int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportName { get; set; }
    }
}