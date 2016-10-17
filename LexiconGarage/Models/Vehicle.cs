using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LexiconGarage.Models {

    public enum VehicleType {
        MotorCycle,
        Car,
        Bus,
        AeroPlane,
        Boat
    }

    

    public class Vehicle {
        private DateTime? dateCreated = null;

        public int Id { get; set; }

        [Display(Name = "Fordonstyp")]
        public VehicleType Type { get; set; }

        [Display(Name = "Registreringsnummer")]
        public string RegNo { get; set; }

        [Display(Name = "Färg")]
        public ConsoleColor Color { get; set; }

        [Display(Name = "Parkeringstid")]
        public DateTime  ParkingTime {          //DateTime2 ??
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }

        } 

        [Display(Name = "Antal hjul")]
        public int NumberOfWheels { get; set; }

        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Display(Name = "Vikt")]
        public int Weight { get; set; }
    }
}