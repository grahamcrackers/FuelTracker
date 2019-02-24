using System.Collections.Generic;
using GasTracker.Data.Models;

namespace GasTracker.Services
{
    public interface IUserService
    {
        void AddUser(User entity);
        IEnumerable<User> GetUsers();
    };
}