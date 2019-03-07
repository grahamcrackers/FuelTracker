using System.Collections.Generic;
using System.Linq;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;
using GasTracker.API.Services;
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

        public Vehicle Add(Vehicle entity)
        {
            var added = _ctx.Vehicles.Add(entity);
            _ctx.SaveChanges();

            return added.Entity;
        }

        public Vehicle Get(int id)
        {
            return _ctx.Vehicles.Where(x => x.VehicleId == id)
                             .FirstOrDefault();
        }

        public IEnumerable<Vehicle> Get()
        {
            return _ctx.Vehicles.AsEnumerable();
        }

        public void Delete(Vehicle entity)
        {
            var existing = _ctx.Vehicles.Where(x => x.VehicleId == entity.VehicleId).FirstOrDefault();
            if (existing != null) _ctx.Vehicles.Remove(existing);
            _ctx.SaveChanges();
        }

        public void Update(Vehicle entity)
        {
            _ctx.Vehicles.Update(entity);
            _ctx.SaveChanges();
        }
    }
}