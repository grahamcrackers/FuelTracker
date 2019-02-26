using System.Collections.Generic;
using System.Linq;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;
using GasTracker.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly TrackerContext _ctx;

        public VehicleService(TrackerContext context)
        {
            _ctx = context;
        }

        public void Add(Vehicle entity)
        {
            _ctx.Vehicles.Add(entity);
            _ctx.SaveChanges();
        }

        public Vehicle Get(int id)
        {
            return _ctx.Vehicles.Where(x => x.VehicleId == id)
                             .FirstOrDefault();
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _ctx.Vehicles.AsEnumerable();
        }

        public void Delete(Vehicle entity)
        {
            var existing = _ctx.Vehicles.Where(x => x.VehicleId == entity.VehicleId).FirstOrDefault();
            if (existing != null) _ctx.Vehicles.Remove(existing);
        }

        public void Update(Vehicle entity)
        {
            _ctx.Vehicles.Update(entity);
            _ctx.SaveChanges();
        }
    }
}