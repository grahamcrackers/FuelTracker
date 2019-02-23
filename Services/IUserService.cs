using System.Collections.Generic;
using GasTracker.Models;
using GasTracker.Repositories.Interfaces;

namespace GasTracker.Services
{
    public interface IUserService
    {
        void AddUser(User entity);
        IEnumerable<User> GetUsers();
    };
}