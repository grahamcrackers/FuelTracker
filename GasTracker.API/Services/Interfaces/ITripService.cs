using System.Collections.Generic;
using GasTracker.API.Data.Models;

namespace GasTracker.API.Services
{
    public interface ITripService
    {
        Trip Add(Trip entity);
        void Delete(Trip entity);
        Trip Get(int id);
        IEnumerable<Trip> Get();
        void Update(Trip entity);
    };
}