using System.Collections.Generic;
using System.Linq;
using GasTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Services
{
    public class UserService : IUserService
    {
        private readonly TrackerContext _ctx;

        public UserService(TrackerContext context)
        {
            _ctx = context;
        }

        public void AddUser(User entity)
        {
            _ctx.Users.Add(entity);
            _ctx.SaveChanges();
        }

        public IEnumerable<User> GetUsers()
        {
            return _ctx.Users.Include(x => x.Vehicles).AsEnumerable();
        }
    }
}