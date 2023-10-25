using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlannerBackend.Logic;

namespace FlightPlanner.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(FlightPlannerDbContext context) : base(context)
        {
            
        }

        public int Create(T entity)
        {
            return Create<T>(entity);
        }

        public void Delete(int id)
        {
            Delete<T>(id);       
        }

        public IEnumerable<T> Get()
        {
            return Get<T>();
        }

        public T? GetById(int id)
        {
            return GetById<T>(id);
        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }

        public IQueryable<T> QueryById(int id)
        {
            return QueryById<T>(id);
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }
    }
}
