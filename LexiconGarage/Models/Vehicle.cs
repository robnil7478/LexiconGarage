using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {
    public enum VehicleType {
        MotorCycle,
        Car,
        Bus,
        AeroPlane,
        Boat
    }

    public class Vehicle {

        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string RegNo { get; set; }
        public ConsoleColor Color { get; set; }
        public DateTime ParkingTime { get; set; } //DateTime2 ?? 
        [Range(0, int.MaxValue, ErrorMessage = "Antalet hjul måste vara >= 0")]
        public int NumberOfWheels { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Vikten i kg, måste vara >= 0")]
        public int Weight { get; set; }
    }
}