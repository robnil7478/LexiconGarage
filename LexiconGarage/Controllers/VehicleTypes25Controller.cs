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
    public class VehicleTypes25Controller : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: VehicleTypes25
        public ActionResult Index()
        {
            return View(db.VehicleTypes.ToList());
        }

        // GET: VehicleTypes25/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicleType = db.VehicleTypes.Find(id);
            if (vehicleType == null)
            {
                return HttpNotFound();
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes25/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypes25/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeInSwedish")] VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                if (!TypeAlreadyExists(vehicleType.TypeInSwedish)) {
                    db.VehicleTypes.Add(vehicleType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } else {
                    ViewBag.ErrorMessage = "Felmeddelande: Det finns redan en fordonstyp '" + vehicleType.TypeInSwedish + "' sparad.";
                }
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes25/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicleType = db.VehicleTypes.Find(id);
            if (vehicleType == null)
            {
                return HttpNotFound();
            }
            return View(vehicleType);
        }

        // POST: VehicleTypes25/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeInSwedish")] VehicleType vehicleType)
        {
            if (ModelState.IsValid) {
                var oldTypeStr = db.VehicleTypes
                    .Where(t => t.Id == vehicleType.Id)
                    .Select(t => t.TypeInSwedish).FirstOrDefault();
                if (oldTypeStr == vehicleType.TypeInSwedish ||
                    !TypeAlreadyExists(vehicleType.TypeInSwedish)) {
                    db.Entry(vehicleType).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } else {
                    ViewBag.ErrorMessage = "Felmeddelande: Det finns redan en fordonstyp '" + vehicleType.TypeInSwedish + "' sparad.";
                }
            }
            return View(vehicleType);
        }

        // GET: VehicleTypes25/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicleType = db.VehicleTypes.Find(id);
            if (vehicleType == null)
            {
                return HttpNotFound();

            } else {
                if (vehicleType.NumberOfVehicles != 0) {
                    ViewBag.ErrorMessage = "Felmeddelande: Fordonstypen '" + vehicleType.TypeInSwedish + "' kan inte tas bort eftersom fordon av typen finns parkerade.";
                    return View("Index", db.VehicleTypes.ToList());
                }
            }
            return View(vehicleType);
        }

        // POST: VehicleTypes25/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleType vehicleType = db.VehicleTypes.Find(id);
            db.VehicleTypes.Remove(vehicleType);
            db.SaveChanges();
            ViewBag.InfoMessage = "Fordonstypen '" + vehicleType.TypeInSwedish + "' har tagits bort.";
            return View("Index", db.VehicleTypes.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeAlreadyExists(string type) {
            var oldType = db.VehicleTypes
                .Where(t => t.TypeInSwedish.ToUpper() == type.ToUpper())
                .FirstOrDefault();
            return oldType != null;
        }
    }
}
