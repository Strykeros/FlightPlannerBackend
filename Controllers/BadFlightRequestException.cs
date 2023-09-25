using System.Runtime.Serialization;

namespace FlightPlannerBackend.Controllers
{
    [Serializable]
    internal class BadFlightRequestException : Exception
    {
        public BadFlightRequestException()
        {
        }

        public BadFlightRequestException(string? message) : base(message)
        {
        }

        public BadFlightRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BadFlightRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}