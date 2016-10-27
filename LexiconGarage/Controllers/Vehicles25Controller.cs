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
        public ActionResult AllVehicles(string RegNo, string brand, string color)
        {
            var allVehicles = db.Vehicles.ToList();
            var dictVehicleTypes = new Dictionary<int, string>();

            ViewBag.SearchTableInfo = "Totalt antal fordon i garaget: " + allVehicles.Count.ToString();
            ViewBag.VBagVehicleList = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish");
            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle, Dictionary<int, string>>(allVehicles, new Vehicle(), dictVehicleTypes);
            return View("Search", tuple);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetSearch(string RegNo, string brand, string color)
        {
            var anEmptyList = new List<Vehicle>();
            var dictVehicleTypes = new Dictionary<int, string>();
            ViewBag.VBagVehicleList = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish");
            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle, Dictionary<int, string>>(anEmptyList, new Vehicle(), dictVehicleTypes);
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

        // GET: Vehicles25/CheckInForMember
        public ActionResult CheckInForMember([Bind(Include = "MemberId")] Vehicle vehicle) {
            var UserName = db.Members.Where(m => m.Id == vehicle.MemberId).Select(m => m.UserName).FirstOrDefault();
            if (UserName != null) {
                ViewBag.UserName = UserName;
                ViewBag.MemberFixed = true;
            }
            else {
                ViewBag.MemberFixed = false;
                ViewBag.MemberId = new SelectList(db.Members, "Id", "UserName");
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish");
            return View("CheckIn", vehicle);
        }

        // GET: Vehicles25/CheckIn
        public ActionResult CheckIn() {
            ViewBag.MemberFixed = false;
            ViewBag.MemberId = new SelectList(db.Members, "Id", "UserName");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish");
            return View();
        }

        // POST: Vehicles25/CheckIn
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
                    return RedirectToAction("Details", new { id = vehicle.Id });
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
                    return RedirectToAction("Details", new { id = vehicle.Id });
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

        public ActionResult Search(string regNo, string brand, string color)
        {
            var emptyVehicleList = new List<Vehicle>();
            var listVehicleTypes = db.VehicleTypes.ToList();
            // @DropDownListFor() alt 1
            ViewBag.VBagVehicleList = new SelectList(listVehicleTypes, "Id", "TypeInSwedish");

            // @DropDownListFor() alt 2  <-- Används inte nu
            var dictVehicleTypes = new Dictionary<int, string>();
            //for (int i = 0; i < listVehicleTypes.Count; i++)
            //{
            //    dictVehicleTypes[i] = listVehicleTypes[i].TypeInSwedish;
            //}
            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle, Dictionary<int, string>>(emptyVehicleList, new Vehicle(), dictVehicleTypes);

            ViewBag.SearchTableInfo = string.Empty;
            return View("Search", tuple);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchVehicles([Bind(Include = "Id,VehicleTypeId,RegNo,MemberId,NumberOfWheels,Brand,Model,Weight,ParkingSlot")] Vehicle vehicle)
        {           
            int intVehicleTypeId;
            var vehicleTypes = db.VehicleTypes.ToList();
            var dictVehicleTypes = new Dictionary<int, string>();
            bool result = false;

            var subsetListOfVehicles = new List<Vehicle>();
            string strRegNo = Request.Form["Item2.RegNo"];
            string strUserName = Request.Form["Item2.Member.UserName"];
            string strBrand = Request.Form["Item2.Brand"];
            string strType = Request.Form["VBagVehicleList"];

            if (vehicle != null)
            {
                vehicle = new Vehicle();
                vehicle.Member = new Member();
            }
            // Populerar sök-värdena till vehicle-ojektet, då behåller vi användarens 
            // inmatade värden då Search-sidan återladdas med resultatlistan/tabellen 
            vehicle.RegNo = string.IsNullOrEmpty(strRegNo) ? "" : strRegNo;
            vehicle.Brand = string.IsNullOrEmpty(strBrand) ? "" : strBrand;
            vehicle.Member.UserName = string.IsNullOrEmpty(strUserName) ? "" : strUserName;
            // enum VehicleType med värde = 0 ('Ange fordonstyp') => söksträng 'strType' 
            // ska ges värdet tomma strängen. 
            strType = (string.IsNullOrEmpty(strType) || strType.Equals("0")) ? "" : strType;
            result = Int32.TryParse(strType, out intVehicleTypeId);
            if (result)
            {
                vehicle.VehicleType = db.VehicleTypes.Find(intVehicleTypeId);
            }

            if (strRegNo.Length > 0 || strUserName.Length > 0 || strBrand.Length > 0 || strType.Length > 0)
                subsetListOfVehicles = (from x in db.Vehicles.ToList()
                                        where ((Int32)x.VehicleTypeId).ToString().ToUpper().Contains(strType.ToUpper()) &&
                                        x.Brand.ToUpper().Contains(strBrand.ToUpper()) &&
                                        x.Member.UserName.ToUpper().Contains(strUserName.ToUpper()) &&
                                        x.RegNo.ToUpper().Contains(strRegNo.ToUpper())
                                        select x).ToList();

            var tuple = new Tuple<IEnumerable<Vehicle>, Vehicle, Dictionary<int, string>>(subsetListOfVehicles, vehicle, dictVehicleTypes);
            ViewBag.SearchTableInfo = "Antal matchande poster: " + subsetListOfVehicles.Count.ToString();
            ViewBag.VBagVehicleList = new SelectList(db.VehicleTypes, "Id", "TypeInSwedish", intVehicleTypeId);
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
