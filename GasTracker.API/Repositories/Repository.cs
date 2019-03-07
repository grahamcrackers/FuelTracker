using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.API.Repositories
{
    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : class
    {
        public Repository(DbContext context) : base(context)
        {
        }

        public T Add(T entity)
        {
            var added = _dbSet.Add(entity);
            return added.Entity;
        }

        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }


        public void Delete(T entity)
        {
            var existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<T> Get()
        {
            return _dbSet;
                    //.AsNoTracking()
                    //.AsEnumerable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet
                    .Where(predicate);
                    //.AsNoTracking()
                    //.AsEnumerable();
        }

        public T Update(T entity)
        {
            var updated = _dbSet.Update(entity);
            return updated.Entity;
        }

        public void Update(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }


        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}