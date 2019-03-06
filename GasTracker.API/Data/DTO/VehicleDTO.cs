using System.Collections.Generic;

namespace GasTracker.API.Data.DTO
{
    public class VehicleDTO
    {
        public int? Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
        public int UserId { get; set; }
        // public ICollection<TripDTO> Trips { get; set; }
    }
}