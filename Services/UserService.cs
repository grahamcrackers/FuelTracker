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

        public User GetUser(int id)
        {
            return _ctx.Users.Include(x => x.Vehicles)
                            .Where(x => x.UserId == id)
                            .FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _ctx.Users.Include(x => x.Vehicles).AsEnumerable();
        }

        public void DeleteUser(User entity)
        {
            var existing = _ctx.Users.Find(entity);
            if (existing != null) _ctx.Users.Remove(existing);
        }

        public void UpdateUser(User entity)
        {
            _ctx.Users.Update(entity);
            _ctx.SaveChanges();
        }
    }
}