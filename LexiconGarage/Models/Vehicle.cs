using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {
    public enum VehicleType {
        [Display(Name ="Motorcykel")]
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

        public const string SweReqErrorString = "Detta fält är obligatoriskt";

        public int Id { get; set; }
        [Required(ErrorMessage = SweReqErrorString)]
        public VehicleType Type { get; set; }
        [Required(ErrorMessage = SweReqErrorString)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "6 tecken, utan mellanslag")]
        public string RegNo { get; set; }
        [Required(ErrorMessage = SweReqErrorString)]
        public ConsoleColor Color { get; set; }
        public DateTime ParkingTime { get; set; } //DateTime2 XXX?? 
        [Required(ErrorMessage = SweReqErrorString)]
        [Range(0, int.MaxValue, ErrorMessage = "Antalet hjul måste vara >= 0")]
        public int NumberOfWheels { get; set; }
        [Required(ErrorMessage = SweReqErrorString)]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Max 30 tecken")]
        public string Brand { get; set; }
        [Required(ErrorMessage = SweReqErrorString)]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Max 30 tecken")]
        public string Model { get; set; }
        [Required(ErrorMessage = SweReqErrorString)]
        [Range(1, int.MaxValue, ErrorMessage = "Vikten i kg, ett heltal > 0")]
        public int Weight { get; set; }
    }


}