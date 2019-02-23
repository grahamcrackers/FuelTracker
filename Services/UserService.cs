using System.Collections.Generic;
using GasTracker.Models;
using GasTracker.Repositories.Interfaces;

namespace GasTracker.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _repo;

        public UserService(IUnitOfWork unit, IRepository<User> repo)
        {
            _uow = unit;
            _repo = repo;
        }

        public void AddUser(User entity)
        {
            _repo.Add(entity);
            _uow.Commit();

        }

        public IEnumerable<User> GetUsers()
        {
            var users = _repo.Get();
            return users;
        }
    }
}