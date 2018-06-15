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
    public class ResponsiblePersonsController : Controller
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();

        // GET: ResponsiblePersons
        public ActionResult Index()
        {
            var responsiblePersons = db.ResponsiblePersons.Include(r => r.Client);
            return View(responsiblePersons.ToList());
        }

        // GET: ResponsiblePersons/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResponsiblePerson responsiblePerson = db.ResponsiblePersons.Find(id);
            if (responsiblePerson == null)
            {
                return HttpNotFound();
            }
            return View(responsiblePerson);
        }

        // GET: ResponsiblePersons/Create
        public ActionResult Create()
        {
            ViewBag.ClientKey = new SelectList(db.Clients, "ClientKey", "OrganisationName");
            return View();
        }

        // POST: ResponsiblePersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResponsiblePersonKey,FirstName,LastName,Patronymic,ClientKey,PhoneNumber,Email,AdditionalInfo,Deleted")] ResponsiblePerson responsiblePerson)
        {
            if (ModelState.IsValid)
            {
                responsiblePerson.ResponsiblePersonKey = Guid.NewGuid();
                db.ResponsiblePersons.Add(responsiblePerson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientKey = new SelectList(db.Clients, "ClientKey", "OrganisationName", responsiblePerson.ClientKey);
            return View(responsiblePerson);
        }

        // GET: ResponsiblePersons/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResponsiblePerson responsiblePerson = db.ResponsiblePersons.Find(id);
            if (responsiblePerson == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientKey = new SelectList(db.Clients, "ClientKey", "OrganisationName", responsiblePerson.ClientKey);
            return View(responsiblePerson);
        }

        // POST: ResponsiblePersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponsiblePersonKey,FirstName,LastName,Patronymic,ClientKey,PhoneNumber,Email,AdditionalInfo,Deleted")] ResponsiblePerson responsiblePerson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(responsiblePerson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientKey = new SelectList(db.Clients, "ClientKey", "OrganisationName", responsiblePerson.ClientKey);
            return View(responsiblePerson);
        }

        // GET: ResponsiblePersons/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResponsiblePerson responsiblePerson = db.ResponsiblePersons.Find(id);
            if (responsiblePerson == null)
            {
                return HttpNotFound();
            }
            return View(responsiblePerson);
        }

        // POST: ResponsiblePersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ResponsiblePerson responsiblePerson = db.ResponsiblePersons.Find(id);
            db.ResponsiblePersons.Remove(responsiblePerson);
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
