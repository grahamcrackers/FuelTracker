using System.Collections.Generic;
using System.Linq;
using GasTracker.Data.Models;
using GasTracker.Repositories;
using GasTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Services
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

        public void Delete(User entity)
        {
            var existing = _uow.GetRepository<User>().Get(x => x.UserId == entity.UserId);
            if (existing != null) _uow.GetRepository<User>().Delete(existing);
        }

        public void Update(User entity)
        {
            _uow.GetRepository<User>().Update(entity);
            _uow.SaveChanges();
        }
    }
}