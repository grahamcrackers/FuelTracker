using System.Collections.Generic;
using System.Linq;
using GasTracker.Data.Models;
using GasTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Services
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