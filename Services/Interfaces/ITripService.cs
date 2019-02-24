using System.Collections.Generic;
using GasTracker.Data.Models;

namespace GasTracker.Services.Interfaces
{
    public interface ITripService
    {
        void Add(Trip entity);
        void Delete(Trip entity);
        Trip Get(int id);
        IEnumerable<Trip> GetAll();
        void Update(Trip entity);
    };
}