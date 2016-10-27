using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {
    public class Member {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Telefonnummer")]
        public string TelNumber { get; set; }

        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Display(Name = "Parkerade fordon")]
        public virtual List<Vehicle> Vehicles { get; set; }

        [Display(Name = "Antal fordon")]
        public virtual int NumberOfVehicles {
            get { return Vehicles == null ? 0 : Vehicles.Count; }
        }
    }
}