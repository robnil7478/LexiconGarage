﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconGarage.DAL;
using LexiconGarage.Models;

namespace LexiconGarage.Controllers {
    public class Vehicles25Controller : Controller {
        private GarageContext db = new GarageContext();

        // GET: Vehicles25
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            return View();
        }

        // GET: Vehicles25/AllVehicles
        public ActionResult AllVehicles() {
            var vehicles = db.Vehicles.Include(v => v.Member).Include(v => v.VehicleType);
            return View(vehicles.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllVehicles(string RegNo, string brand, string color) {
            var allVehicles = db.Vehicles.ToList();
            ViewBag.SearchTableInfo = "Totalt antal fordon i garaget: " + allVehicles.Count.ToString();

            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle>(allVehicles, new Vehicle());
            return View("Search", tuple);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetSearch(string RegNo, string brand, string color) {
            var anEmptyList = new List<Vehicle>();

            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle>(anEmptyList, new Vehicle());
            return View("Search", tuple);
        }


        // GET: Vehicles25/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles25/Create
        public ActionResult CheckIn() {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "UserName");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish");
            return View();
        }

        // POST: Vehicles25/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn([Bind(Include = "Id,VehicleTypeId,RegNo,MemberId,NumberOfWheels,Brand,Model,Weight,ParkingSlot")] Vehicle vehicle) // XXX Behövs Exclude ParkingTíme också?
        {
            if (ModelState.IsValid) {
                if (!RegNoAlreadyCheckedIn(vehicle.RegNo)) {
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("AllVehicles");
                } else {
                    ViewBag.ErrorMessage = "Felmeddelande: Det finns redan ett fordon " +
                        "med registreringsnummer " + vehicle.RegNo + " registrerat.";
                }
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "UserName", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles25/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "UserName", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles25/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
           /* [Bind(Include = "Id,VehicleTypeId,RegNo,MemberId,ParkingTime,NumberOfWheels,Brand,Model,Weight,ParkingSlot")]*/
            [Bind(Exclude = "ParkingTime")] Vehicle vehicle) {
            if (ModelState.IsValid) {
                var oldRegNo = db.Vehicles.Where(v => v.Id == vehicle.Id).Select(v => v.RegNo).FirstOrDefault();
                var oldParkTime = db.Vehicles.Where(v => v.Id == vehicle.Id).Select(v => v.ParkingTime).FirstOrDefault();
                if (oldRegNo == vehicle.RegNo || !RegNoAlreadyCheckedIn(vehicle.RegNo)) {
                    vehicle.ParkingTime = oldParkTime;
                    db.Entry(vehicle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AllVehicles");
                }
                ViewBag.ErrorMessage = "Felmeddelande: Det finns redan ett fordon " +
                        "med registreringsnummer " + vehicle.RegNo + " registrerat.";
                return View(vehicle);
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "UserName", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles25/CheckOut/5
        public ActionResult CheckOut(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles25/CheckOut/5
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutConfirmed(int id) {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            Receipt receipt = new Receipt(vehicle);
            // Remove vehicle
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return View("Receipt", receipt);
        }

        public ActionResult Search(string regNo, string brand, string color) {
            var anEmptyList = new List<Vehicle>();
            var vehicleTypes = db.VehicleTypes.ToList();
            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle, IEnumerable<VehicleType>>(anEmptyList, new Vehicle(), vehicleTypes);
            ViewBag.SearchTableInfo = string.Empty;
            return View("Search", tuple);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchVehicles([Bind(Include =
            "Id,Type,RegNo,Color,NumberOfWheels,Brand,Model,Weight")] Vehicle vehicle)
        //public ActionResult SearchVehicles()
        {
            int intVehicleType;
            var vehicleTypes = db.VehicleTypes.ToList();
            bool result = false;

            var subsetListOfVehicles = new List<Vehicle>();
            string strRegNo = Request.Form["Item2.RegNo"];
            string strOwner = Request.Form["Item2.Owner"];
            string strBrand = Request.Form["Item2.Brand"];
            string strType = Request.Form["Item2.Type"];

            if (vehicle != null) {
                vehicle = new Vehicle();
            }
            // Populerar sök-värdena till vehicle-ojektet, då behåller vi användarens 
            // inmatade värden då Search-sidan återladdas med resultatlistan/tabellen 
            vehicle.RegNo = string.IsNullOrEmpty(strRegNo) ? "" : strRegNo;
            vehicle.Brand = string.IsNullOrEmpty(strBrand) ? "" : strBrand;
            vehicle.Member.UserName = string.IsNullOrEmpty(strOwner) ? "" : strOwner;
            // enum VehicleType med värde = 0 ('Ange fordonstyp') => söksträng 'strType' 
            // ska ges värdet tomma strängen. 
            strType = (string.IsNullOrEmpty(strType) || strType.Equals("0")) ? "" : strType;
            result = Int32.TryParse(strType, out intVehicleType);
            //if (result) {
            //    vehicle.VehicleType = (VehicleType)intVehicleType;
            //}

            if (strRegNo.Length > 0 || strOwner.Length > 0 || strBrand.Length > 0 || strType.Length > 0)
                subsetListOfVehicles = (from x in db.Vehicles.ToList()
                                        where ((Int32)x.VehicleTypeId).ToString().ToUpper().Contains(strType.ToUpper()) &&
                                        x.Brand.ToUpper().Contains(strBrand.ToUpper()) &&
                                        x.Member.UserName.ToUpper().Contains(strOwner.ToUpper()) &&
                                        x.RegNo.ToUpper().Contains(strRegNo.ToUpper())
                                        select x).ToList();

            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle, IEnumerable<VehicleType>>(subsetListOfVehicles, vehicle, vehicleTypes);
            ViewBag.SearchTableInfo = "Antal matchande poster: " + subsetListOfVehicles.Count.ToString();
            return View("Search", tuple);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool RegNoAlreadyCheckedIn(string regNo) {
            var count = db.Vehicles.Where(v => v.RegNo == regNo).ToList().Count;
            return count != 0;
        }
    }
}
