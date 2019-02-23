using System;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Repositories
{
    public abstract class BaseRepository<T>  where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }
    }
}