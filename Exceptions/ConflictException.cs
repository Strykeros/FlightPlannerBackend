namespace FlightPlannerBackend.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base("Conflict (409)")
        {
            
        }
    }
}
