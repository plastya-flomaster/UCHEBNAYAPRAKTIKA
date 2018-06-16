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
    public class ZakupkasController : Controller
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();

        // GET: Zakupkas
        public ActionResult Index()
        {
            var zakupkas = db.Zakupkas.Include(z => z.Adress).Include(z => z.Auction).Include(z => z.ResponsiblePerson).Include(z => z.Status).Include(z => z.TimeZone);
            return View(zakupkas.ToList());
        }

        // GET: Zakupkas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakupka zakupka = db.Zakupkas.Find(id);
            if (zakupka == null)
            {
                return HttpNotFound();
            }
            return View(zakupka);
        }

        // GET: Zakupkas/Create
        public ActionResult Create()
        {
            ViewBag.AdressKey = new SelectList(db.Adresses, "AdressKey", "House");
            ViewBag.AuctionKey = new SelectList(db.Auctions, "AuctionKey", "AuctionName");
            ViewBag.ResponsiblePersonKey = new SelectList(db.ResponsiblePersons, "ResponsiblePersonKey", "FirstName");
            ViewBag.StatusKey = new SelectList(db.Status, "StatusKey", "StatusString");
            ViewBag.TimeZoneKey = new SelectList(db.TimeZones, "TimeZoneKey", "TimeZone1");
            return View();
        }

        // POST: Zakupkas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZakupkaKey,WorkTitle,ContractSum,CashCover,Deadline,ResponsiblePersonKey,TimeZoneKey,AuctionKey,AdressKey,StatusKey,Deleted")] Zakupka zakupka)
        {
            if (ModelState.IsValid)
            {
                zakupka.ZakupkaKey = Guid.NewGuid();
                db.Zakupkas.Add(zakupka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdressKey = new SelectList(db.Adresses, "AdressKey", "House", zakupka.AdressKey);
            ViewBag.AuctionKey = new SelectList(db.Auctions, "AuctionKey", "AuctionName", zakupka.AuctionKey);
            ViewBag.ResponsiblePersonKey = new SelectList(db.ResponsiblePersons, "ResponsiblePersonKey", "FirstName", zakupka.ResponsiblePersonKey);
            ViewBag.StatusKey = new SelectList(db.Status, "StatusKey", "StatusString", zakupka.StatusKey);
            ViewBag.TimeZoneKey = new SelectList(db.TimeZones, "TimeZoneKey", "TimeZone1", zakupka.TimeZoneKey);
            return View(zakupka);
        }

        // GET: Zakupkas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakupka zakupka = db.Zakupkas.Find(id);
            if (zakupka == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdressKey = new SelectList(db.Adresses, "AdressKey", "House", zakupka.AdressKey);
            ViewBag.AuctionKey = new SelectList(db.Auctions, "AuctionKey", "AuctionName", zakupka.AuctionKey);
            ViewBag.ResponsiblePersonKey = new SelectList(db.ResponsiblePersons, "ResponsiblePersonKey", "FirstName", zakupka.ResponsiblePersonKey);
            ViewBag.StatusKey = new SelectList(db.Status, "StatusKey", "StatusString", zakupka.StatusKey);
            ViewBag.TimeZoneKey = new SelectList(db.TimeZones, "TimeZoneKey", "TimeZone1", zakupka.TimeZoneKey);
            return View(zakupka);
        }

        // POST: Zakupkas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZakupkaKey,WorkTitle,ContractSum,CashCover,Deadline,ResponsiblePersonKey,TimeZoneKey,AuctionKey,AdressKey,StatusKey,Deleted")] Zakupka zakupka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zakupka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdressKey = new SelectList(db.Adresses, "AdressKey", "House", zakupka.AdressKey);
            ViewBag.AuctionKey = new SelectList(db.Auctions, "AuctionKey", "AuctionName", zakupka.AuctionKey);
            ViewBag.ResponsiblePersonKey = new SelectList(db.ResponsiblePersons, "ResponsiblePersonKey", "FirstName", zakupka.ResponsiblePersonKey);
            ViewBag.StatusKey = new SelectList(db.Status, "StatusKey", "StatusString", zakupka.StatusKey);
            ViewBag.TimeZoneKey = new SelectList(db.TimeZones, "TimeZoneKey", "TimeZone1", zakupka.TimeZoneKey);
            return View(zakupka);
        }

        // GET: Zakupkas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakupka zakupka = db.Zakupkas.Find(id);
            if (zakupka == null)
            {
                return HttpNotFound();
            }
            return View(zakupka);
        }

        // POST: Zakupkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Zakupka zakupka = db.Zakupkas.Find(id);
            db.Zakupkas.Remove(zakupka);
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
