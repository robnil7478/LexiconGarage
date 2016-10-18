using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {

    public enum VehicleType {
        [Display(Name = "Välj fordonstyp:")]
        None,
        [Display(Name = "Motorcykel")]
        MotorCycle,
        [Display(Name = "Bil")]
        Car,
        [Display(Name = "Buss")]
        Bus,
        [Display(Name = "Flygplan")]
        AeroPlane,
        [Display(Name = "Båt")]
        Boat
    }
    
    public class Vehicle {
        private DateTime? dateCreated = null;

        public const string SweReqErrorString = "Detta fält är obligatoriskt";

        public int Id { get; set; }

        [Display(Name = "Fordonstyp")]
        [Required(ErrorMessage = SweReqErrorString)]
        [Range(1, int.MaxValue, ErrorMessage = "Du måste välja fordonstyp")]
        public VehicleType Type { get; set; }

        [Display(Name = "Registreringsnummer")]
        [Required(ErrorMessage = SweReqErrorString)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "6 tecken, utan mellanslag")]
        public string RegNo { get; set; }

        [Display(Name = "Ägare")]
        [Required(ErrorMessage = SweReqErrorString)]
        public string Owner { get; set; }

        [Display(Name = "Parkeringstid")]
        public DateTime  ParkingTime {          //DateTime2 ??
            get
            {
                return this.dateCreated.HasValue ? this.dateCreated.Value : DateTime.Now;
            }

            set { this.dateCreated = value; }
        } 

        [Display(Name = "Antal hjul")]
        [Required(ErrorMessage = SweReqErrorString)]
        [Range(0, int.MaxValue, ErrorMessage = "Antalet hjul måste vara >= 0")]
        [RegularExpression("[0-9]", ErrorMessage = "Heltal > 0")]
        public int NumberOfWheels { get; set; }

        [Display(Name = "Märke")]
        [Required(ErrorMessage = SweReqErrorString)]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Max 30 tecken")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        [Required(ErrorMessage = SweReqErrorString)]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Max 30 tecken")]
        public string Model { get; set; }

        [Display(Name = "Vikt (kg)")]
        [Required(ErrorMessage = SweReqErrorString)]
        [Range(1, int.MaxValue, ErrorMessage = "Vikten i kg, ett heltal > 0")]
        [RegularExpression("[0-9]", ErrorMessage = "Heltal > 0")]
        public int Weight { get; set; }
    }


}