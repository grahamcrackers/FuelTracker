using System.Collections.Generic;

namespace GasTracker.API.Services
{
    public interface IService<T>
    {
        void Add(T entity);
        void Delete(int id);
        T Get(int id);
        IEnumerable<T> Get();
        void Update(T entity);
    }
}