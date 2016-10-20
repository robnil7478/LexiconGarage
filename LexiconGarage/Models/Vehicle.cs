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
        [Display(Name = "Bil")]
        Car,
        [Display(Name = "Buss")]
        Bus,
        [Display(Name = "Båt")]
        Boat,
        [Display(Name = "Flygplan")]
        AeroPlane,
        [Display(Name = "Motorcykel")]
        MotorCycle
    }
    
    public class Vehicle {
        private DateTime? parkingTime = null;

        public const string SweReqErrorString = "Detta fält är obligatoriskt";

        private string regNo;

        public int Id { get; set; }

        [Display(Name = "Fordonstyp")]
        [Required(ErrorMessage = SweReqErrorString)]
        [Range(1, int.MaxValue, ErrorMessage = "Du måste välja fordonstyp")]
        public VehicleType Type { get; set; }

        [Display(Name = "Reg.nr")]
        [Required(ErrorMessage = SweReqErrorString)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "6 tecken, utan mellanslag")]
        public string RegNo {
           get { return regNo; }
            set { regNo = value.ToUpper(); }
        }
       
        [Display(Name = "Ägare")]
        [Required(ErrorMessage = SweReqErrorString)]
        public string Owner { get; set; }

        [Display(Name = "Parkerades")]
        public DateTime  ParkingTime {
            get
            {
                return this.parkingTime.HasValue ? this.parkingTime.Value : DateTime.Now;
            }
            set { this.parkingTime = value; }
        } 

        [Display(Name = "Antal hjul")]
        [Required(ErrorMessage = SweReqErrorString)]
        [Range(0, int.MaxValue, ErrorMessage = "Antalet hjul måste vara >= 0")]
        [RegularExpression("[0-9]+", ErrorMessage = "Heltal > 0")]
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
        [RegularExpression("[0-9]+", ErrorMessage = "Heltal > 0")]
        public int Weight { get; set; }

        [Display(Name = "Platsnummer")]
        [Range(1, int.MaxValue, ErrorMessage = "Ett heltal > 0")]
        
        public int ParkingSlot { get; set; }

    }


}