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
    public class VehicleControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public VehicleControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task can_get_vehicles()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/vehicles");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(stringResponse);
            Assert.Contains(vehicles, v => v.Make == "Jeep");
            Assert.Contains(vehicles, v => v.Model == "Escape");
        }

        [Fact]
        public async Task can_get_single_vehicle()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/vehicles/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var vehicle = JsonConvert.DeserializeObject<Vehicle>(stringResponse);
            Assert.True(vehicle.Make == "Jeep");
        }

        [Fact]
        public async Task can_add_vehicle()
        {
            var newVehicle = new Vehicle()
            {
                Make = "Subaru",
                Model = "BRZ",
                UserId = 1
            };

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync("/api/vehicles", getBodyJson<Vehicle>(newVehicle));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var vehicle = JsonConvert.DeserializeObject<Vehicle>(stringResponse);
            Assert.True(vehicle.Make == "Subaru");
            Assert.True(vehicle.UserId > 0);
        }

        [Fact]
        public async Task can_update_vehicle()
        {
            // Get the user
            var httpResponse = await _client.GetAsync("/api/vehicles/1");
            httpResponse.EnsureSuccessStatusCode();
            
            var getResponse = await httpResponse.Content.ReadAsStringAsync();
            var vehicle = JsonConvert.DeserializeObject<Vehicle>(getResponse);
            vehicle.UserId = 2;

            // The endpoint or route of the controller action.
            httpResponse = await _client.PutAsync("/api/vehicles/1", getBodyJson(vehicle));
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var putResponse = await httpResponse.Content.ReadAsStringAsync();
            var updated = JsonConvert.DeserializeObject<Vehicle>(putResponse);
            Assert.True(updated.UserId == 2);
        }

        [Fact]
        public async Task can_delete_vehicle()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync("/api/vehicles/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            Assert.True(httpResponse.IsSuccessStatusCode);
        }

        private StringContent getBodyJson<T>(T user)
        {
            return new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        }
    }
}
