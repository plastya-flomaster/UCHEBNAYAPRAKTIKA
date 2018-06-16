using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UCHEBNAYAPRAKTIKA.Models;
using PagedList.Mvc;
using PagedList;

namespace UCHEBNAYAPRAKTIKA.Controllers
{
    public class ResponsiblePersonsController : Controller
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();

        // GET: ResponsiblePersons
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.CompanySortParm = sortOrder == "Comp" ? "comp_desc" : "Comp";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var responsiblePersons = db.ResponsiblePersons.Where(n => n.Deleted == false).Include(r => r.Client);

            if (!String.IsNullOrEmpty(searchString))
            {
                responsiblePersons = responsiblePersons.Where(s => s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString)
                                       || s.Patronymic.Contains(searchString)
                                       || s.Email.Contains(searchString)
                                       || s.Client.OrganisationName.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    responsiblePersons = responsiblePersons.OrderByDescending(s => s.LastName);
                    break;
                case "email_desc":
                    responsiblePersons = responsiblePersons.OrderByDescending(s => s.Email);
                    break;
                case "Comp":
                    responsiblePersons = responsiblePersons.OrderBy(s => s.Client.OrganisationName);
                    break;
                case "comp_desc":
                    responsiblePersons = responsiblePersons.OrderByDescending(s => s.Client.OrganisationName);
                    break;
                case "Email":
                    responsiblePersons = responsiblePersons.OrderBy(s => s.Email);
                    break;

                default:  // Name ascending 
                    responsiblePersons = responsiblePersons.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(responsiblePersons.ToPagedList(pageNumber, pageSize));
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
            if (ModelState.IsValid && !IfExists(responsiblePerson))
            {
                responsiblePerson.ResponsiblePersonKey = Guid.NewGuid();
                db.ResponsiblePersons.Add(responsiblePerson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientKey = new SelectList(db.Clients, "ClientKey", "OrganisationName", responsiblePerson.ClientKey);
            return View(responsiblePerson);
        }

        private bool IfExists(ResponsiblePerson responsiblePerson)
        {
            if (db.ResponsiblePersons.Any(a => a.FirstName == responsiblePerson.FirstName
             && a.LastName == responsiblePerson.LastName
             && a.Patronymic == responsiblePerson.Patronymic
             && a.Email == responsiblePerson.Email
              && a.PhoneNumber == responsiblePerson.PhoneNumber
              && a.ClientKey == responsiblePerson.ClientKey
              && a.AdditionalInfo == responsiblePerson.AdditionalInfo
             && a.Deleted == responsiblePerson.Deleted)) return true;
            return false;
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
            responsiblePerson.Deleted = true;
            db.Entry(responsiblePerson).State = EntityState.Modified;
            //   db.ResponsiblePersons.Remove(responsiblePerson);
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
