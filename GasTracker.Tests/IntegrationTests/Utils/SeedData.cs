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

            dbContext.Users.Add(jon);
            dbContext.Users.Add(dany);
            dbContext.Vehicles.Add(jeep);
            dbContext.Vehicles.Add(escape);
            
            dbContext.SaveChanges();
        }
    }
}