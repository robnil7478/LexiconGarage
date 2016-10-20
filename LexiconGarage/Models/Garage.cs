using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {
    public class Garage {
        public int Id { get; set; }
        public int AmountOfSlots { get; set; }
        public int EmptySlots { get; set; }
        public int Rate { get; set; }
        public GarageStatistics Statistics { get; set; }

        public Garage() : this(10,1) {
        }
        public Garage(int amountOfSlots = 10, int rate = 1) { // XXX Utöka antal platser!
            AmountOfSlots = amountOfSlots;
            EmptySlots = amountOfSlots;
            Rate = rate;
            Statistics = new GarageStatistics();
        }

        internal void AddVehicleInStat(Vehicle vehicle) {
            Statistics.AddVehicle(vehicle);
        }
        internal void RemoveVehicleInStat(Vehicle vehicle) {
            Statistics.RemoveVehicle(vehicle);
        }
    }
}