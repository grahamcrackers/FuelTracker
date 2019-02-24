using System.Collections.Generic;
using GasTracker.Data.Models;

namespace GasTracker.Services.Interfaces
{
    public interface IUserService
    {
        void AddUser(User entity);
        void DeleteUser(User entity);
        User GetUser(int id);
        IEnumerable<User> GetUsers();

        void UpdateUser(User entity);
    };
}