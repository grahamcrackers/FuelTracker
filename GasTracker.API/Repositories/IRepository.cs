using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace GasTracker.API.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {

        T Add(T entity);
        void Add(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        T Update(T entity);
        void Update(IEnumerable<T> entities);
    }
}