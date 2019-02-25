using System.Collections.Generic;

namespace GasTracker.Services
{
    public interface IService<T>
    {
        void Add(T entity);
        void Delete(T entity);
        T Get(int id);
        IEnumerable<T> Get();
        void Update(T entity);
    }
}