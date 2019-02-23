using System;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Interfaces {
    public interface IUnitOfWork: IDisposable {
        DbContext Context { get; }
        void Commit();
    }
}