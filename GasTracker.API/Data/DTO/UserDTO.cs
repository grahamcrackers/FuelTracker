using System.ComponentModel.DataAnnotations;

namespace GasTracker.API.Data.DTO {
    public class UserDTO {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}