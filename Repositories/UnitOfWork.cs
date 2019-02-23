using Microsoft.EntityFrameworkCore;
using GasTracker.Repositories.Interfaces;
using GasTracker.Models;

namespace GasTracker.Repositories {
    public class UnitOfWork : IUnitOfWork
    {
        public TrackerContext Context { get; }

        public UnitOfWork(TrackerContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}