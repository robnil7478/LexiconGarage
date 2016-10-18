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
        public ActionResult CheckIn([Bind(Include = "Id,Type,RegNo,Color,NumberOfWheels,Brand,Model,Weight",Exclude = "ParkingTime")] Vehicle vehicle)
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
            //string searchRegNo = Request.Form["Item2.RegNo"];
            int intNumber;
            bool result = false;

            if (vehicle != null){
                vehicle = new Vehicle();
            }

            vehicle.RegNo = string.IsNullOrEmpty(Request.Form["Item2.RegNo"]) ? "" : Request.Form["Item2.RegNo"];
            vehicle.Brand = string.IsNullOrEmpty(Request.Form["Item2.Brand"]) ? "" : Request.Form["Item2.Brand"];

            string strType = string.IsNullOrEmpty(Request.Form["Item2.Type"]) ? "" : Request.Form["Item2.Type"];
            result = Int32.TryParse(strType, out intNumber);
            if (result) {
                vehicle.Type = (VehicleType)intNumber;
            }

            var subsetListOfVehicles = new List<Vehicle>();
            if (vehicle.RegNo.Length > 0 || vehicle.Brand.Length > 0 || vehicle.Type.ToString().Length > 0)
                subsetListOfVehicles = (from x in db.Vehicles.ToList()
                                        where x.Type.ToString().ToUpper().Contains(vehicle.Type.ToString().ToUpper()) &&
                                        x.Brand.ToUpper().Contains(vehicle.Brand.ToUpper()) &&
                                        x.RegNo.ToUpper().Contains(vehicle.RegNo.ToUpper())
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
