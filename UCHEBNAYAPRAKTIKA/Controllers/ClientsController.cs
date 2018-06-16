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
    public class ClientsController : Controller
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();
       
              // GET: Clients
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Inn" ? "inn_desc" : "Inn";
                ViewBag.KppSortParm = sortOrder == "Kpp" ? "kpp_desc" : "Kpp";


            if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var clients = from s in db.Clients
                               select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    clients = clients.Where(s => s.OrganisationName.Contains(searchString)
                                           || s.INN.Contains(searchString)
                                           || s.KPP.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        clients = clients.OrderByDescending(s => s.OrganisationName);
                        break;
                    case "inn_desc":
                        clients = clients.OrderByDescending(s => s.INN);
                        break;
                    case "Inn":
                    clients = clients.OrderByDescending(s => s.INN);
                        break;
                    case "kpp_desc":
                    clients = clients.OrderByDescending(s => s.KPP);
                        break;
                    case "Kpp":
                        clients = clients.OrderByDescending(s => s.KPP);
                        break;

                    default:  // Name ascending 
                        clients = clients.OrderBy(s => s.OrganisationName);
                        break;
                }

                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(clients.ToPagedList(pageNumber, pageSize));
            }
 

        // GET: Clients/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientKey,OrganisationName,INN,KPP,Deleted")] Client client)
        {
            if (ModelState.IsValid && !IfExists(client))
            {
                client.ClientKey = Guid.NewGuid();
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        private bool IfExists(Client cl)
        {
            if (db.Clients.Any(a => a.OrganisationName == cl.OrganisationName
              && a.INN == cl.INN
               && a.KPP == cl.KPP
              && a.Deleted == cl.Deleted)) return true;
            return false;
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientKey,OrganisationName,INN,KPP,Deleted")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Client client = db.Clients.Find(id);
            client.Deleted = true;
            db.Entry(client).State = EntityState.Modified;
          //  db.Clients.Remove(client);
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
