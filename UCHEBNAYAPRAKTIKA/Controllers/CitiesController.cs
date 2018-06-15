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
    public class CitiesController : Controller
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();

        // GET: Cities
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            var cities = db.Cities.Include(c => c.Region);

            if (!String.IsNullOrEmpty(searchString))
            {
                cities = cities.Where(s => s.CityName.Contains(searchString)
                                       || s.Region.RegionName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    cities = cities.OrderByDescending(s => s.CityName);
                    break;
                case "Reg":
                    cities = cities.OrderBy(s => s.Region.RegionName);
                    break;
                case "reg_desc":
                    cities = cities.OrderByDescending(s => s.Region.RegionName);
                    break;
                default:  // Name ascending 
                    cities = cities.OrderBy(s => s.CityName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(cities.ToPagedList(pageNumber, pageSize));
        }

        // GET: Cities/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            ViewBag.RegionKey = new SelectList(db.Regions, "RegionKey", "RegionName");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityKey,RegionKey,CityName,Deleted")] City city)
        {
            if (ModelState.IsValid && !IfExists(city))
            {
                city.CityKey = Guid.NewGuid();
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RegionKey = new SelectList(db.Regions, "RegionKey", "RegionName", city.RegionKey);
            return View(city);
        }
        private bool IfExists(City city)
        {
            if (db.Cities.Any(a => a.CityName == city.CityName
              && a.RegionKey == city.RegionKey
              && a.Deleted == city.Deleted)) return true;
            return false;
        }
      
        // GET: Cities/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionKey = new SelectList(db.Regions, "RegionKey", "RegionName", city.RegionKey);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityKey,RegionKey,CityName,Deleted")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegionKey = new SelectList(db.Regions, "RegionKey", "RegionName", city.RegionKey);
            return View(city);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            City city = db.Cities.Find(id);
            city.Deleted = true;
            db.Entry(city).State = EntityState.Modified;
            // db.Cities.Remove(city);
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
