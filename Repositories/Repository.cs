using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GasTracker.Repositories.Interfaces;

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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Repository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}