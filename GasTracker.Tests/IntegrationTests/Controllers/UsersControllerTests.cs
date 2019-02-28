using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GasTracker.API;
using GasTracker.API.Data.Models;
using IntegrationTests.Utils;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UsersControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task can_get_users()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/users");

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

        [Fact]
        public async Task can_add_user()
        {
            var newUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Username = "tuser",
                Email = "tuser@test.com"
            };

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync("/api/users", getBodyJson(newUser));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            Assert.True(httpResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task can_update_user()
        {
            var newUser = new User()
            {
                FirstName = "Jon",
                LastName = "Targaryen",
                Username = "jtargaryen",
                Email = "lordcommander@weirwood.net"
            };
            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync("/api/users/1", getBodyJson(newUser));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            Assert.True(httpResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task can_delete_user()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync("/api/users/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            Assert.True(httpResponse.IsSuccessStatusCode);
        }

        private StringContent getBodyJson(User user)
        {
            return new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        }
    }
}
