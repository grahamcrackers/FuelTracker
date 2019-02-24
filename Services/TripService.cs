using System.Collections.Generic;
using System.Linq;
using GasTracker.Data.Models;
using GasTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Services
{
    public class TripService : ITripService
    {
        private readonly TrackerContext _ctx;

        public TripService(TrackerContext context)
        {
            _ctx = context;
        }

        public void Add(Trip entity)
        {
            _ctx.Trips.Add(entity);
            _ctx.SaveChanges();
        }

        public Trip Get(int id)
        {
            return _ctx.Trips.Where(x => x.TripId == id)
                             .FirstOrDefault();
        }

        public IEnumerable<Trip> GetAll()
        {
            return _ctx.Trips.AsEnumerable();
        }

        public void Delete(Trip entity)
        {
            var existing = _ctx.Trips.Where(x => x.TripId == entity.TripId).FirstOrDefault();
            if (existing != null) _ctx.Trips.Remove(existing);
        }

        public void Update(Trip entity)
        {
            _ctx.Trips.Update(entity);
            _ctx.SaveChanges();
        }
    }
}