using System.Collections.Generic;
using GasTracker.Models;
using GasTracker.Repositories.Interfaces;

namespace GasTracker.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork unit)
        {
            _uow = unit;
        }

        public void AddUser(User entity)
        {
            _uow.GetRepository<User>().Add(entity);
            _uow.SaveChanges();
        }

        public IEnumerable<User> GetUsers()
        {
            return _uow.GetRepository<User>().Get();
        }
    }
}