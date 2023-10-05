namespace FlightPlannerBackend.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("Bad request (400)")
        { 
        
        }
    }
}
