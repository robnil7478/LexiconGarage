using System;
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
    public class GarageController : Controller {
        private GarageContext db = new GarageContext();


        // GET: Garage
        public ActionResult Index() {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        // GET: Garage/Details/5
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

        // GET: Garage/CheckIn
        public ActionResult CheckIn() {
            return View();
        }

        // POST: Garage/CheckIn
        // To protect from overposting attacks, please enable the specific properties 
        // you want to bind to, for more details see
        // http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn([Bind(Include = "Id,Type,RegNo,Owner,NumberOfWheels,Brand,Model,Weight", Exclude = "ParkingTime")] Vehicle vehicle) {
            if (ModelState.IsValid) {
                if (! RegNoAlreadyCheckedIn(vehicle.RegNo)) {
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("AllVehicle");
                } else {
                    ViewBag.ErrorMessage = "Felmeddelande: Det finns redan ett fordon " + 
                        "med registreringsnummer " + vehicle.RegNo + " registrerat.";
                }
            }

            return View(vehicle);
        }

        // GET: Garage/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Garage/Edit/5
        // To protect from overposting attacks, please enable the specific properties
        // you want to bind to, for more details see 
        // http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "ParkingTime")] Vehicle vehicle) {
            // För Debug: var errors = ModelState.Values.SelectMany(v => v.Errors); 
            if (ModelState.IsValid) {
                var oldRegNo = db.Vehicles.Where(v => v.Id == vehicle.Id).Select(v => v.RegNo).FirstOrDefault();
                var oldParkTime = db.Vehicles.Where(v => v.Id == vehicle.Id).Select(v => v.ParkingTime).FirstOrDefault();
                if (oldRegNo == vehicle.RegNo || ! RegNoAlreadyCheckedIn(vehicle.RegNo)) {
                    vehicle.ParkingTime = oldParkTime;
                    db.Entry(vehicle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AllVehicle");
                }
                ViewBag.ErrorMessage = "Felmeddelande: Det finns redan ett fordon " +
                        "med registreringsnummer " + vehicle.RegNo + " registrerat.";
                return View(vehicle);
            }
            ViewBag.ErrorMessage = "Felmeddelande: Felaktig inmatning.";
            return View(vehicle);
        }

        // GET: Garage/CheckOut/5
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

        // POST: Garage/Checkout/5
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
            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle>(anEmptyList, 
                new Vehicle());
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
            vehicle.Owner = string.IsNullOrEmpty(strOwner) ? "" : strOwner;
            // enum VehicleType med värde = 0 ('Ange fordonstyp') => söksträng 'strType' 
            // ska ges värdet tomma strängen. 
            strType = (string.IsNullOrEmpty(strType) || strType.Equals("0")) ? "" : strType;
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
            ViewBag.SearchTableInfo = "Antal matchande poster: " + subsetListOfVehicles.Count.ToString();
            return View("Search", tuple);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllVehicles(string RegNo, string brand, string color) {
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


        // GET: List all vehicle in Garage
        public ActionResult AllVehicle()
        {
            return View(db.Vehicles.ToList());
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
