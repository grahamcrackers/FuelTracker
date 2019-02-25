using System.Collections.Generic;
using System.Linq;
using GasTracker.Data.Models;
using GasTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Services
{
    public class TripService : IService<Trip>
    {
        private readonly IUnitOfWork _uow;

        public TripService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(Trip entity)
        {
            _uow.GetRepository<Trip>().Add(entity);
            _uow.SaveChanges();
        }

        public Trip Get(int id)
        {
            return _uow.GetRepository<Trip>().Get(x => x.TripId == id).FirstOrDefault();
        }

        public IEnumerable<Trip> Get()
        {
            return _uow.GetRepository<Trip>().Get();
        }

        public void Delete(int id)
        {
            var existing = _uow.GetRepository<Trip>().Get(x => x.TripId == id);
            if (existing != null) _uow.GetRepository<Trip>().Delete(existing);
        }

        public void Update(Trip entity)
        {
            _uow.GetRepository<Trip>().Update(entity);
            _uow.SaveChanges();
        }
    }
}