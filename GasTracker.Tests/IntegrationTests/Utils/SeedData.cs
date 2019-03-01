using System;
using System.Collections.Generic;
using GasTracker.API.Data.Context;
using GasTracker.API.Data.Models;

namespace IntegrationTests.Utils
{
    public static class SeedData
    {
        public static void PopulateTestData(TrackerContext dbContext)
        {
            User jon = new User()
            {
                FirstName = "Jon",
                LastName = "Snow",
                Username = "jsnow",
                Email = "lordcommander@weirwood.net"
            };

            User dany = new User()
            {
                FirstName = "Daenerys",
                LastName = "Targaryen",
                Username = "dstormborn",
                Email = "DaenerysStormbornoftheHouseTargaryentheFirstofHerNametheUnburntQueenofMeereenQueenoftheAndalsandtheRhoynarandtheFirstMenKhaleesioftheGreatGrassSeaBreakerofChainsandMotherofDragons@weirwood.net"
            };

            Vehicle jeep = new Vehicle(){
                Make = "Jeep",
                Model = "Wrangler",
                UserId = 1
            };

            Vehicle escape = new Vehicle(){
                Make = "Ford",
                Model = "Escape",
                UserId = 2
            };

            Trip trip1 = new Trip(){
                VehicleId = 1,
                Date = DateTime.Now,
                Odometer = 1000,
                TripMeter = 276.8m,
                TotalGallons = 18.73m,
                TotalFuelCost = 44.44m
            };

            Trip trip2 = new Trip(){
                VehicleId = 1,
                Date = DateTime.Now,
                Odometer = 1276,
                TripMeter = 299.3m,
                TotalGallons = 19.72m,
                TotalFuelCost = 49.83m
            };

            dbContext.Users.Add(jon);
            dbContext.Users.Add(dany);
            dbContext.Vehicles.Add(jeep);
            dbContext.Vehicles.Add(escape);
            dbContext.Trips.Add(trip1);
            dbContext.Trips.Add(trip2);
            
            dbContext.SaveChanges();
        }
    }
}