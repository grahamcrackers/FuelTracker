using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GasTracker.API;
using GasTracker.API.Data.Models;
using GasTracker.Tests.Utils;
using Newtonsoft.Json;
using Xunit;

namespace GasTracker.Tests
{
    public class UsersServiceIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UsersServiceIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task can_get_users()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/users/all");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResponse);
            Assert.Contains(users, u => u.FirstName == "Jon");
            Assert.Contains(users, u => u.FirstName == "Daenerys");
        }

        [Fact]
        public async Task can_get_single_user()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/users/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);
            Assert.True(user.FirstName == "Jon");
        }
    }
}
