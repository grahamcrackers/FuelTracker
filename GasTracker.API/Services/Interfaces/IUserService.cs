using System.Collections.Generic;
using GasTracker.API.Data.Models;

namespace GasTracker.API.Services
{
    public interface IUserService
    {
        User Add(User entity);
        void Delete(User entity);
        User Get(int id);
        IEnumerable<User> Get();

        void Update(User entity);
    };
}