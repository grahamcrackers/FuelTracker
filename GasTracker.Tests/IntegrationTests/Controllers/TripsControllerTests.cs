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
            var trip = await getSingleTrip<TripDTO>(1);
            Assert.True(trip.Odometer == 1000);
        }

        [Fact]
        public async Task can_add_trip()
        {
            TripDTO trip3 = new TripDTO()
            {
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
            var trip = JsonConvert.DeserializeObject<TripDTO>(stringResponse);
            Assert.True(trip.Odometer > 0);
            Assert.True(trip.Id > 0);
        }

        [Fact]
        public async Task can_update_trip()
        {
            Trip trip = await getSingleTrip<Trip>(2);
            trip.VehicleId = 2;

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync("/api/trips/1", getBodyJson<Trip>(trip));
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

        private async Task<T> getSingleTrip<T>(int id)
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync($"/api/trips/{id}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        private StringContent getBodyJson<T>(T user)
        {
            return new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        }
    }
}
