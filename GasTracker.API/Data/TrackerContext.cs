using GasTracker.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GasTracker.API.Data.Context
{
    public class TrackerContext : DbContext
    {
        public TrackerContext(DbContextOptions<TrackerContext> options)
            : base(options)
        { 
            this.Users
                .Include(u => u.Vehicles)
                .ToList();

            this.Vehicles
                .Include(v => v.Trips)
                .ToList();
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}