using System.Collections.Generic;
using System.Linq;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;
using GasTracker.API.Services;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.API.Services
{
    public class TripService : ITripService
    {
        private readonly TrackerContext _ctx;

        public TripService(TrackerContext context)
        {
            _ctx = context;
        }

        public Trip Add(Trip entity)
        {
            var added = _ctx.Trips.Add(entity);
            _ctx.SaveChanges();

            return added.Entity;
        }

        public Trip Get(int id)
        {
            return _ctx.Trips.Where(x => x.TripId == id)
                             .FirstOrDefault();
        }

        public IEnumerable<Trip> Get()
        {
            return _ctx.Trips.AsEnumerable();
        }

        public void Delete(Trip entity)
        {
            var existing = _ctx.Trips.Where(x => x.TripId == entity.TripId).FirstOrDefault();
            if (existing != null) _ctx.Trips.Remove(existing);
            _ctx.SaveChanges();
        }

        public void Update(Trip entity)
        {
            _ctx.Trips.Update(entity);
            _ctx.SaveChanges();
        }
    }
}