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
        [Range(0, int.MaxValue, ErrorMessage = "Antalet hjul måste vara >= 0")]
        public int NumberOfWheels { get; set; }

        [Display(Name = "Märke")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Display(Name = "Vikt")]
        [Range(0, int.MaxValue, ErrorMessage = "Vikten i kg, måste vara >= 0")]
        public int Weight { get; set; }
    }
}