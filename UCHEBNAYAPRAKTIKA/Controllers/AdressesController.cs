using PagedList;
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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Reg" ? "reg_desc" : "Reg";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var adresses = db.Adresses.Include(a => a.Street);


            if (!String.IsNullOrEmpty(searchString))
            {
                adresses = adresses.Where(s => s.Postcode.Contains(searchString)
                                       || s.Street.City.CityName.Contains(searchString)
                                       || s.Street.StreetName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    adresses = adresses.OrderByDescending(s => s.Street.StreetName);
                    break;
                case "Reg":
                    adresses = adresses.OrderBy(s => s.Street.City.CityName);
                    break;
                case "reg_desc":
                    adresses = adresses.OrderByDescending(s => s.Street.City.CityName);
                    break;
                default:  // Name ascending 
                    adresses = adresses.OrderBy(s => s.Street.StreetName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(adresses.ToPagedList(pageNumber, pageSize));
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
            ViewBag.CityKey = new SelectList(db.Adresses.Include(a => a.Street.City), "CityKey", "CityName");
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
