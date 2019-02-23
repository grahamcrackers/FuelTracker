using System;
using GasTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Repositories.Interfaces {
    public interface IUnitOfWork: IDisposable {
        TrackerContext Context { get; }
        void Commit();
    }
}