using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GasTracker.API;
using GasTracker.API.Data.DTO;
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
            var users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(stringResponse);
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
            var user = JsonConvert.DeserializeObject<UserDTO>(stringResponse);
            Assert.True(user.FirstName == "Jon");
        }

        [Fact]
        public async Task can_add_user()
        {
            var newUser = new UserDTO()
            {
                FirstName = "Test",
                LastName = "User",
                Username = "tuser",
                Email = "tuser@test.com"
                
            };

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync("/api/users", getBodyJson<UserDTO>(newUser));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(stringResponse);
            Assert.True(user.FirstName == "Test");
            Assert.True(user.Id > 0);
        }

        [Fact]
        public async Task can_update_user()
        {
            // Get the user
            var httpResponse = await _client.GetAsync("/api/users/1");
            httpResponse.EnsureSuccessStatusCode();
            
            var getResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(getResponse);
            user.LastName = "Targaryen";

            // The endpoint or route of the controller action.
            httpResponse = await _client.PutAsync("/api/users/1", getBodyJson<UserDTO>(user));
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var putResponse = await httpResponse.Content.ReadAsStringAsync();
            var updated = JsonConvert.DeserializeObject<UserDTO>(putResponse);
            Assert.True(updated.LastName == "Targaryen");
            Assert.True(updated.Id == 1);
        }

        [Fact]
        public async Task can_delete_user()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync("/api/users/2");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            Assert.True(httpResponse.IsSuccessStatusCode);
        }

        private async Task<UserDTO> getSingleUser(int id)
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync($"/api/users/{id}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDTO>(stringResponse);
        }

        private StringContent getBodyJson<T>(T obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}
