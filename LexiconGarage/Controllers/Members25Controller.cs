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
    public class Members25Controller : Controller {
        private GarageContext db = new GarageContext();

        // GET: Members25
        public ActionResult Index() {
            return View(db.Members.ToList());
        }

        // GET: Members25/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null) {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members25/Register
        public ActionResult Register() {
            return View();
        }

        // POST: Members25/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,UserName,Name,TelNumber,Address")] Member member) {
            if (ModelState.IsValid) {
                if (!UserNameAlreadyRegistered(member.UserName)) {
                    db.Members.Add(member);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { Id = member.Id });
                } else {
                    ViewBag.ErrorMessage = "Felmeddelande: Det finns redan en medlem " +
                        "med användarnamn '" + member.UserName + "' registrerad.";
                }
            }

            return View(member);
        }

        // GET: Members25/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null) {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members25/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Name,TelNumber,Address")] Member member) {
            if (ModelState.IsValid) {
                var oldUserName = db.Members.Where(m => m.Id == member.Id)
                    .Select(m => m.UserName).FirstOrDefault();
                if (oldUserName == member.UserName || !UserNameAlreadyRegistered(member.UserName)) {
                    db.Entry(member).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", new { Id = member.Id });
                }
                ViewBag.ErrorMessage = "Felmeddelande: Det finns redan en medlem " +
                        "med användarnamn '" + member.UserName + "' registrerad.";
            }
            return View(member);
        }

        // GET: Members25/Unregister/5
        public ActionResult Unregister(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null) {
                return HttpNotFound();
            } else {
                if (member.NumberOfVehicles != 0) {
                    ViewBag.ErrorMessage = "Felmeddelande: Medlemmen kan inte avregistreras eftersom fordon finns parkerade.";
                    return View("Details", member);
                }
            }
            return View(member);
        }

        // POST: Members25/Unregister/5
        [HttpPost, ActionName("Unregister")]
        [ValidateAntiForgeryToken]
        public ActionResult UnregisterConfirmed(int id) {
            Member member = db.Members.Find(id);
            var userName = member.UserName;
            db.Members.Remove(member);
            db.SaveChanges();
            ViewBag.InfoMessage = "Medlemmen '" + userName + "' har avregistrerats.";
            return View("Index", db.Members.ToList());
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserNameAlreadyRegistered(string userName) {
            var count = db.Members.Where(m => m.UserName == userName).ToList().Count;
            return count != 0;
        }
    }
}
