using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {
    public class GarageStatistics {
        public int TotalWheels { get; set; }
        public int TotalCars { get; set; }
        public int TotalBusses { get; set; }
        public int TotalBoats { get; set; }
        public int TotalAeroPlanes { get; set; }
        public int TotalMCs { get; set; }
        public int TotalCost { get; set; }

        internal void AddVehicle(Vehicle vehicle) {
            UpdateWheels(0, vehicle.NumberOfWheels);
            UpdateVehicleType(vehicle.Type, 1);
        }
        internal void RemoveVehicle(Vehicle vehicle) {
            UpdateWheels(vehicle.NumberOfWheels, 0);
            UpdateVehicleType(vehicle.Type, -1);
        }

        private void UpdateVehicleType(VehicleType type, int value) {
            switch (type) {
                case VehicleType.AeroPlane:
                    TotalAeroPlanes += value;
                    break;
                case VehicleType.Boat:
                    TotalBoats += value;
                    break;
                case VehicleType.Bus:
                    TotalBusses += value;
                    break;
                case VehicleType.Car:
                    TotalCars += value;
                    break;
                case VehicleType.MotorCycle:
                    TotalMCs += value;
                    break;
            }
        }

        internal void UpdateWheels(int before, int after) {
            TotalWheels = TotalWheels - before + after;
        }

        internal void UpdateCost(Vehicle vehicle, int rate) {
            TotalCost += 
                Receipt.TotalCost(vehicle.ParkingTime, DateTime.Now, rate);
        }
    }
}