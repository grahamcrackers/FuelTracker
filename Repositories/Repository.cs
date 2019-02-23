using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GasTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace GasTracker.Repositories {
    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : class
    {
        public Repository(DbContext context) : base(context)
        {
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<T> Get()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public void Delete(T entity)
        {
            var existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // public void Delete(T entity)
        // {
        //     T existing = _unitOfWork.Context.Set<T>().Find(entity);
        //     if(existing != null) _unitOfWork.Context.Set<T>().Remove(existing);
        // }

        // public IEnumerable<T> Get()
        // {
        //     return _unitOfWork.Context.Set<T>().AsEnumerable<T>();
        // }

        // public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        // {
        //     return _unitOfWork.Context.Set<T>().Where(predicate).AsEnumerable<T>();
        // }

        // public void Update(T entity)
        // {
        //     _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        //     _unitOfWork.Context.Set<T>().Attach(entity);
        // }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}