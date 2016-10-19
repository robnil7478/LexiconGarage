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

namespace LexiconGarage.Controllers
{
    public class GarageController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Garage
        public ActionResult Index()
        {
            return View(db.Vehicles.ToList());
        }

        // GET: Garage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Garage/CheckIn
        public ActionResult CheckIn()
        {
            return View();
        }

        // POST: Garage/CheckIn
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn([Bind(Include = "Id,Type,RegNo,Owner,NumberOfWheels,Brand,Model,Weight",Exclude = "ParkingTime")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Garage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Garage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "ParkingTime")] Vehicle vehicle)
        {
          
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Garage/CheckOut/5
        public ActionResult CheckOut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Garage/Checkout/5
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Search(string regNo, string brand, string color)
        {
            var anEmptyList = new List<Vehicle>();
            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle>(anEmptyList, new Vehicle());
            //return PartialView("Search", tuple);

            return View("Search", tuple);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchVehicles([Bind(Include = "Id,Type,RegNo,Color,NumberOfWheels,Brand,Model,Weight")] Vehicle vehicle)
        //public ActionResult SearchVehicles()
        {
            int intVehicleType;
            bool result = false;
            var subsetListOfVehicles = new List<Vehicle>();
            string strRegNo = Request.Form["Item2.RegNo"];
            string strOwner = Request.Form["Item2.Owner"];
            string strBrand = Request.Form["Item2.Brand"];
            string strType = Request.Form["Item2.Type"];
        
            if (vehicle != null){
                vehicle = new Vehicle();
            }
            // Populerar sök-värdena till vehicle-ojektet, då behåller vi användarens inmatade värden då Search-sidan återladdas med resultatlistan/tabellen 
            vehicle.RegNo = string.IsNullOrEmpty(strRegNo) ? "" : strRegNo;
            vehicle.Brand = string.IsNullOrEmpty(strBrand) ? "" : strBrand;
            vehicle.Owner = string.IsNullOrEmpty(strOwner) ? "" : strOwner;
            // enum VehicleType med värde = 0 ('Ange fordonstyp') => söksträng 'strType' ska ges värdet tomma strängen. 
            strType = ( string.IsNullOrEmpty(strType) || strType.Equals("0") ) ? "" : strType;
            result = Int32.TryParse(strType, out intVehicleType);
            if (result) {
                vehicle.Type = (VehicleType)intVehicleType;
            }

            if (strRegNo.Length > 0 || strOwner.Length > 0 || strBrand.Length > 0 || strType.Length > 0)
                subsetListOfVehicles = (from x in db.Vehicles.ToList()
                                        where ((Int32)x.Type).ToString().ToUpper().Contains(strType.ToUpper()) &&
                                        x.Brand.ToUpper().Contains(strBrand.ToUpper()) &&
                                        x.Owner.ToUpper().Contains(strOwner.ToUpper()) &&
                                        x.RegNo.ToUpper().Contains(strRegNo.ToUpper())
                                        select x).ToList();

            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle>(subsetListOfVehicles, vehicle);
            return View("Search", tuple);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllVehicles(string RegNo, string brand, string color)
        {
            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle>(db.Vehicles.ToList(), new Vehicle());
            return View("Search", tuple);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetSearch(string RegNo, string brand, string color)
        {
            var anEmptyList = new List<Vehicle>();

            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle>(anEmptyList, new Vehicle());
            return View("Search", tuple);
        }

        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
