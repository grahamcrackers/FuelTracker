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
        { }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }

    }

}