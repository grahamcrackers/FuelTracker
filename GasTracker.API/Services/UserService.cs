using System.Collections.Generic;
using System.Linq;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;
using GasTracker.API.Services;
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

        public User Add(User entity)
        {
            var added = _ctx.Users.Add(entity);
            _ctx.SaveChanges();

            return added.Entity;
        }

        public User Get(int id)
        {
            return _ctx.Users.Include(x => x.Vehicles)
                            .Where(x => x.UserId == id)
                            .FirstOrDefault();
        }

        public IEnumerable<User> Get()
        {
            return _ctx.Users.Include(x => x.Vehicles).AsEnumerable();
        }

        public void Delete(User entity)
        {
            var existing = _ctx.Users.Where(x => x.UserId == entity.UserId).FirstOrDefault();
            if (existing != null) _ctx.Users.Remove(existing);
            _ctx.SaveChanges();
        }

        public void Update(User entity)
        {
            _ctx.Users.Update(entity);
            _ctx.SaveChanges();
        }
    }
}