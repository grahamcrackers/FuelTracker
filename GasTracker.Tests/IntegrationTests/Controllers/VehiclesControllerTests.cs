using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GasTracker.API;
using GasTracker.API.Data.DTO;
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
            var vehicles = JsonConvert.DeserializeObject<IEnumerable<VehicleDTO>>(stringResponse);
            Assert.Contains(vehicles, v => v.Make == "Jeep");
        }

        [Fact]
        public async Task can_get_single_vehicle()
        {
            var vehicle = await getSingleVehicle(1);
            Assert.True(vehicle.Make == "Jeep");
        }

        [Fact]
        public async Task can_add_vehicle()
        {
            var newVehicle = new VehicleDTO()
            {
                Make = "Subaru",
                Model = "BRZ",
                UserId = 1
            };

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync("/api/vehicles", getBodyJson<VehicleDTO>(newVehicle));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var vehicle = JsonConvert.DeserializeObject<VehicleDTO>(stringResponse);
            Assert.True(vehicle.Make == "Subaru");
            Assert.True(vehicle.UserId > 0);
        }

        [Fact]
        public async Task can_update_vehicle()
        {
            VehicleDTO vehicle = await getSingleVehicle(1);
            vehicle.UserId = 2;

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync("/api/vehicles/1", getBodyJson<VehicleDTO>(vehicle));
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var putResponse = await httpResponse.Content.ReadAsStringAsync();
            var updated = JsonConvert.DeserializeObject<VehicleDTO>(putResponse);
            Assert.True(updated.UserId == 2);
        }

        [Fact]
        public async Task can_delete_vehicle()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync("/api/vehicles/2");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            Assert.True(httpResponse.IsSuccessStatusCode);
        }

        private async Task<VehicleDTO> getSingleVehicle(int id)
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync($"/api/vehicles/{id}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VehicleDTO>(stringResponse);
        }

        private StringContent getBodyJson<T>(T user)
        {
            return new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        }
    }
}
