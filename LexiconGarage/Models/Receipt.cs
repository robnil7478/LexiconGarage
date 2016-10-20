using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {
    public class Receipt {

        [Display(Name = "Reg.nr")]
        public string RegNo { get; set; }
        [Display(Name = "Fordonstyp")]
        public VehicleType VehicleType { get; set; }
        [Display(Name = "Ägare")]
        public string Owner { get; set; }
        [Display(Name = "Parkerad")]
        public DateTime FromTime { get; set; }
        [Display(Name = "Uthämtad")]
        public DateTime ToTime { get; set; }
        [Display(Name = "Total parkeringstid")]
        public string TotalTime { get; set; }
        [Display(Name = "Taxa")]
        public int Rate { get; set; }
        [Display(Name = "Pris")]
        public int Price { get; set; }

        public Receipt(Vehicle vehicle) {
            RegNo = vehicle.RegNo;
            VehicleType = vehicle.Type;
            Owner = vehicle.Owner;
            FromTime = vehicle.ParkingTime;
            ToTime = DateTime.Now;
            TimeSpan totalTime = (ToTime - FromTime);
            TotalTime = FormatTime(totalTime);
            Rate = 1; // 60 kr/ hour
            Price = Rate * (int) totalTime.TotalMinutes;
        }

        public string FormatTime(TimeSpan totalTime) {
            int days = totalTime.Days;
            int hours = totalTime.Hours;
            int minutes = totalTime.Minutes;
            return days + " dagar, " + hours + " timmar, " + 
                   minutes + " minuter";
        }
    }
}