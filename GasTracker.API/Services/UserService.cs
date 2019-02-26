using System.Collections.Generic;
using System.Linq;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;
using GasTracker.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.API.Services
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
            var existing = _ctx.Users.Where(x => x.UserId == entity.UserId).FirstOrDefault();
            if (existing != null) _ctx.Users.Remove(existing);
        }

        public void UpdateUser(User entity)
        {
            _ctx.Users.Update(entity);
            _ctx.SaveChanges();
        }
    }
}