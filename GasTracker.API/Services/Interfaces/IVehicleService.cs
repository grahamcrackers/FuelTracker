using System.Collections.Generic;
using GasTracker.API.Data.Models;

namespace GasTracker.API.Services.Interfaces
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