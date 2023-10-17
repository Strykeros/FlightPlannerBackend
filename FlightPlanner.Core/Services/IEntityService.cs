using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        IQueryable<T> Query();
        IQueryable<T> QueryById(int id);
        IEnumerable<T> Get();
        T GetById(int id);
        int Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
