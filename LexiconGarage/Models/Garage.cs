using LexiconGarage.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconGarage.Models {
    public class Garage {

        static readonly int MaxSlots = 14;
        static readonly int HourPrice = 60;
        public int TotalSlots { get { return totalSlots; } }
        public int Price { get { return price; } }

        private GarageContext context;
        private int totalSlots;
        private int price;

        public Garage(GarageContext db) {
            context = db;
            totalSlots = MaxSlots;
            price = HourPrice; // kr/h
        }
        public int EmptySlots() {
            return TotalSlots - context.Vehicles.Count();
        }
    }
}