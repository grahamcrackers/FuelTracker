using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GasTracker.API.Data.Models;

namespace GasTracker.API.Services
{
    public interface IBaseService<T>
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Add(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
                
        void Update(T entity);
        void Update(IEnumerable<T> entities);
    };
}