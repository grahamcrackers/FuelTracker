using System.Collections.Generic;
using System.Linq;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;
using GasTracker.API.Repositories;
using GasTracker.API.Services;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.API.Services
{
    public class VehicleService : IService<Vehicle>
    {
        private readonly IUnitOfWork _uow;

        public VehicleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(Vehicle entity)
        {
            _uow.GetRepository<Vehicle>().Add(entity);
            _uow.SaveChanges();
        }

        public Vehicle Get(int id)
        {
            return _uow.GetRepository<Vehicle>().Get(x => x.VehicleId == id).FirstOrDefault();
        }

        public IEnumerable<Vehicle> Get()
        {
            return _uow.GetRepository<Vehicle>().Get();
        }

        public void Delete(int id)
        {
            var existing = _uow.GetRepository<Vehicle>().Get(x => x.VehicleId == id);
            if (existing != null) _uow.GetRepository<Vehicle>().Delete(existing);
        }

        public void Update(Vehicle entity)
        {
            _uow.GetRepository<Vehicle>().Update(entity);
            _uow.SaveChanges();
        }
    }
}