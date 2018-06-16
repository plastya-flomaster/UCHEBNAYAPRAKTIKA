using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UCHEBNAYAPRAKTIKA.Models;

namespace UCHEBNAYAPRAKTIKA.Controllers
{
    public class AdressesController : Controller
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();

        // GET: Adresses
        public ActionResult Index()
        {
            var adresses = db.Adresses.Include(a => a.Street);
            return View(adresses.ToList());
        }

        // GET: Adresses/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adress adress = db.Adresses.Find(id);
            if (adress == null)
            {
                return HttpNotFound();
            }
            return View(adress);
        }

        // GET: Adresses/Create
        public ActionResult Create()
        {
            ViewBag.StreetKey = new SelectList(db.Streets, "StreetKey","StreetName");
            return View();
        }

        // POST: Adresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdressKey,StreetKey,House,Buildung,Housing,Office,Postcode,Deleted")] Adress adress)
        {
            if (ModelState.IsValid)
            {
                adress.AdressKey = Guid.NewGuid();
                db.Adresses.Add(adress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StreetKey = new SelectList(db.Streets, "StreetKey", "StreetName", adress.StreetKey);
            return View(adress);
        }

        // GET: Adresses/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adress adress = db.Adresses.Find(id);
            if (adress == null)
            {
                return HttpNotFound();
            }
            ViewBag.StreetKey = new SelectList(db.Streets, "StreetKey", "StreetName", adress.StreetKey);
            return View(adress);
        }

        // POST: Adresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdressKey,StreetKey,House,Buildung,Housing,Office,Postcode,Deleted")] Adress adress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StreetKey = new SelectList(db.Streets, "StreetKey", "StreetName", adress.StreetKey);
            return View(adress);
        }

        // GET: Adresses/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adress adress = db.Adresses.Find(id);
            if (adress == null)
            {
                return HttpNotFound();
            }
            return View(adress);
        }

        // POST: Adresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Adress adress = db.Adresses.Find(id);
            db.Adresses.Remove(adress);
            db.SaveChanges();
            return RedirectToAction("Index");
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
