using System.Collections.Generic;
using System.Linq;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;
using GasTracker.API.Repositories;
using GasTracker.API.Services;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.API.Services
{
    public class UserService : IService<User>
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(User entity)
        {
            _uow.GetRepository<User>().Add(entity);
            _uow.SaveChanges();
        }

        public User Get(int id)
        {
            return _uow.GetRepository<User>().Get(x => x.UserId == id).FirstOrDefault();
        }

        public IEnumerable<User> Get()
        {
            return _uow.GetRepository<User>().Get();
        }

        public void Delete(int id)
        {
            var existing = _uow.GetRepository<User>().Get(x => x.UserId == id);
            if (existing != null) _uow.GetRepository<User>().Delete(existing);
        }

        public void Update(User entity)
        {
            _uow.GetRepository<User>().Update(entity);
            _uow.SaveChanges();
        }
    }
}