using System;
using System.Collections.Generic;
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
        public int NumberOfWheels { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Weight { get; set; }
    }
}