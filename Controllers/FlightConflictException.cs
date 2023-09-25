using System.Runtime.Serialization;

namespace FlightPlannerBackend.Controllers
{
    [Serializable]
    internal class FlightConflictException : Exception
    {
        public FlightConflictException()
        {
        }

        public FlightConflictException(string? message) : base(message)
        {
        }

        public FlightConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FlightConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}