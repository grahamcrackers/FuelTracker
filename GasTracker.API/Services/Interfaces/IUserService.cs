using System.Collections.Generic;
using GasTracker.API.Data.Models;

namespace GasTracker.API.Services.Interfaces
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