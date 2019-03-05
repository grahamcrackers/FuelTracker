using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasTracker.API.Data.DTO {
    public class TripDTO {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public int Odometer { get; set; }
        public decimal TripMeter { get; set; }
        public decimal TotalGallons { get; set; }
        public decimal TotalFuelCost { get; set; }
        public decimal CostPerGallon { get; set; }
        public decimal MilesPerGallon { get; set; }
        public decimal CostPerMile { get; set; }
        public int VehicleId { get; set; }
    }
}