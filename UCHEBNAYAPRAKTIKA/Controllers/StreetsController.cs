﻿using PagedList;
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
    public class StreetsController : Controller
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();

        // GET: Streets
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var streets = db.Streets.Include(s => s.City);
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

            var str = db.Streets.Where(a => a.Deleted != true).Include(c => c.City);

            if (!String.IsNullOrEmpty(searchString))
            {
                str = str.Where(s => s.StreetName.Contains(searchString)
                                       || s.City.CityName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    str = str.OrderByDescending(s => s.StreetName);
                    break;
                case "Reg":
                    str = str.OrderBy(s => s.City.CityName);
                    break;
                case "reg_desc":
                    str = str.OrderByDescending(s => s.City.CityName);
                    break;
                default:  // Name ascending 
                    str = str.OrderBy(s => s.StreetName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(str.ToPagedList(pageNumber, pageSize));
        }

        // GET: Streets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Street street = db.Streets.Find(id);
            if (street == null)
            {
                return HttpNotFound();
            }
            return View(street);
        }

        // GET: Streets/Create
        public ActionResult Create()
        {
            ViewBag.CityKey = new SelectList(db.Cities, "CityKey", "CityName");
            return View();
        }

        // POST: Streets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StreetKey,CityKey,StreetName,Deleted")] Street street)
        {
            if (ModelState.IsValid)
            {
                street.StreetKey = Guid.NewGuid();
                db.Streets.Add(street);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityKey = new SelectList(db.Cities, "CityKey", "CityName", street.CityKey);
            return View(street);
        }

        // GET: Streets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Street street = db.Streets.Find(id);
            if (street == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityKey = new SelectList(db.Cities, "CityKey", "CityName", street.CityKey);
            return View(street);
        }

        // POST: Streets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StreetKey,CityKey,StreetName,Deleted")] Street street)
        {
            if (ModelState.IsValid)
            {
                db.Entry(street).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityKey = new SelectList(db.Cities, "CityKey", "CityName", street.CityKey);
            return View(street);
        }

        // GET: Streets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Street street = db.Streets.Find(id);
            if (street == null)
            {
                return HttpNotFound();
            }
            return View(street);
        }

        // POST: Streets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Street street = db.Streets.Find(id);
            db.Streets.Remove(street);
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
