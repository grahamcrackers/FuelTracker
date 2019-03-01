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
    public class TripsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TripsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task can_get_trips()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/trips");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            System.Console.WriteLine(stringResponse);
            var trips = JsonConvert.DeserializeObject<IEnumerable<Trip>>(stringResponse);
            Assert.Contains(trips, t => t.Odometer > 0);
            Assert.Contains(trips, t => t.TripMeter > 0);
        }

        [Fact]
        public async Task can_get_single_trip()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/trips/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var trip = JsonConvert.DeserializeObject<Trip>(stringResponse);
            Assert.True(trip.Odometer == 1000);
        }

        [Fact]
        public async Task can_add_trip()
        {
            Trip trip3 = new Trip(){
                VehicleId = 1,
                Date = DateTime.Now.Date,
                Odometer = 1578,
                TripMeter = 150.9m,
                TotalGallons = 10.58m,
                TotalFuelCost = 23.21m
            };

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync("/api/trips", getBodyJson(trip3));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var trip = JsonConvert.DeserializeObject<Trip>(stringResponse);
            Assert.True(trip.Odometer > 0);
            Assert.True(trip.TripId > 0);
        }

        [Fact]
        public async Task can_update_trip()
        {
            // Get the user
            var httpResponse = await _client.GetAsync("/api/trips/1");
            httpResponse.EnsureSuccessStatusCode();
            
            var getResponse = await httpResponse.Content.ReadAsStringAsync();
            var trip = JsonConvert.DeserializeObject<Trip>(getResponse);
            trip.VehicleId = 2;

            // The endpoint or route of the controller action.
            httpResponse = await _client.PutAsync("/api/trips/1", getBodyJson(trip));
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var putResponse = await httpResponse.Content.ReadAsStringAsync();
            var updated = JsonConvert.DeserializeObject<Trip>(putResponse);
            Assert.True(updated.VehicleId == 2);
        }

        [Fact]
        public async Task can_delete_trip()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync("/api/trips/1");

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
