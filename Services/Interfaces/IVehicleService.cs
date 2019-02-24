using System.Collections.Generic;
using GasTracker.Data.Models;

namespace GasTracker.Services.Interfaces
{
    public interface IVehicleService
    {
        void Add(Vehicle entity);
        void Delete(Vehicle entity);
        Vehicle Get(int id);
        IEnumerable<Vehicle> GetAll();
        void Update(Vehicle entity);
    };
}