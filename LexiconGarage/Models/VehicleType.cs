using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {
    public class VehicleType {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Fordonstyp")]
        public string TypeInSwedish { get; set; }

        [Display(Name = "Antal parkerade fordon av denna typ")]
        public virtual int NumberOfVehicles { get { return Vehicles.Count; } }

        [Display(Name = "Fordon av denna typ")]
        public virtual List<Vehicle> Vehicles { get; set; }
    }
}