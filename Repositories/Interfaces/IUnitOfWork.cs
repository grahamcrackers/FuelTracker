using System;
using GasTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Repositories.Interfaces {
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        // IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
        // IRepositoryReadOnly<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class;

        int SaveChanges();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}