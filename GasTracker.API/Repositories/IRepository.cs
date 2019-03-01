using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using GasTracker.API.Repositories.Paging;

namespace GasTracker.API.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IDisposable where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        void Add(params T[] entities);
        void Add(IEnumerable<T> entities);


        void Delete(T entity);
        void Delete(object id);
        void Delete(params T[] entities);
        void Delete(IEnumerable<T> entities);
        
        
        T Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
    }
}