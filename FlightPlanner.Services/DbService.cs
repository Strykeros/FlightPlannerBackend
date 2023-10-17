using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlannerBackend.Logic;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class DbService : IDbService
    {
        protected FlightPlannerDbContext _context;
        private static readonly object _lockObject = new object();
        public DbService(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public int Create<T>(T entity) where T : Entity
        {
            lock (_lockObject) 
            { 
                if(entity is Flight flight)
                {
                    bool flightIsNull = string.IsNullOrEmpty(flight.ArrivalTime) ||
                            string.IsNullOrEmpty(flight.DepartureTime) ||
                            string.IsNullOrEmpty(flight.Carrier) ||
                            flight.From == null ||
                             string.IsNullOrEmpty(flight.From.Country) ||
                             string.IsNullOrEmpty(flight.From.City) ||
                             string.IsNullOrEmpty(flight.From.AirportName) ||
                            flight.To == null ||
                             string.IsNullOrEmpty(flight.To.Country) ||
                             string.IsNullOrEmpty(flight.To.City) ||
                             string.IsNullOrEmpty(flight.To.AirportName);

                    bool flightExists = _context.Set<Flight>().Any(existingFlight =>
                        existingFlight.From.AirportName == flight.From.AirportName &&
                        existingFlight.From.City == flight.From.City &&
                        existingFlight.From.Country == flight.From.Country &&
                        existingFlight.To.AirportName == flight.To.AirportName &&
                        existingFlight.To.City == flight.To.City &&
                        existingFlight.To.Country == flight.To.Country &&
                        existingFlight.Carrier == flight.Carrier &&
                        existingFlight.DepartureTime == flight.DepartureTime &&
                        existingFlight.ArrivalTime == flight.ArrivalTime &&
                        existingFlight.To.AirportName == flight.To.AirportName
                    );

                    bool dateMismatch = DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime);

                    if (flight.From.AirportName.ToLower().Trim() == flight.To.AirportName.ToLower().Trim() || flightIsNull || dateMismatch)
                        return 400;

                    if (flightExists)
                        return 409;

                    _context.Set<T>().Add(entity);
                    _context.SaveChanges();
                }

                return 201;            
            }
        }

        public void Delete<T>(int id) where T : Entity
        {
            var query = GetById<T>(id);

            if (query != null)
            {
                _context.Set<T>().Remove(query);
                _context.SaveChanges();
            }
        }

        public void DeleteAll<T>() where T : Entity
        {
            lock (_lockObject)
            {
                _context.Set<T>().RemoveRange(_context.Set<T>());
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> Get<T>() where T : Entity
        {
            lock (_lockObject)
            {
                if (typeof(T) == typeof(Flight))
                {
                    return _context.Set<T>().Include("From").Include("To")
                        .ToList();
                }

                return _context.Set<T>().ToList();
            }
        }

        public T GetById<T>(int id) where T : Entity
        {
            if (typeof(T) == typeof(Flight))
            {
                return _context.Set<T>().Include("From").Include("To")
                    .SingleOrDefault(x => x.Id == id);
            }

            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> QueryById<T>(int id) where T : Entity
        {
            return _context.Set<T>().Where(e => e.Id == id);
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
