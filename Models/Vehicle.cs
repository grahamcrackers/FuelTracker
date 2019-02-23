using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasTracker.Models {
    [Table("vehicle")]
    public class Vehicle {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Vin { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}