using System.Collections.Generic;
using GasTracker.API.Data.Models;

namespace GasTracker.API.Services
{
    public interface IVehicleService
    {
        Vehicle Add(Vehicle entity);
        void Delete(Vehicle entity);
        Vehicle Get(int id);
        IEnumerable<Vehicle> Get();
        void Update(Vehicle entity);
    };
}