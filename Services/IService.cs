using System.Collections.Generic;

namespace GasTracker.Services
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