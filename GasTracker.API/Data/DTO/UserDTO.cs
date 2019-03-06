using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GasTracker.API.Data.DTO {
    public class UserDTO {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        // public ICollection<VehicleDTO> Vehicles { get; set; }
    }
}