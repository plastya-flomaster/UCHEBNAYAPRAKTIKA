using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCHEBNAYAPRAKTIKA.Models;

namespace UCHEBNAYAPRAKTIKA.Controllers
{
    public class HomeController : Controller
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();

        public ActionResult RegionCity()
        {
            List<Region> reg = new List<Region>();
            reg = db.Regions.ToList();
            ViewBag.RegionList = new SelectList(reg, "RegionKey", "RegionName");
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

       public JsonResult GetCities(Guid RegionKey)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<City> cities = db.Cities.Where(a => a.RegionKey == RegionKey).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
       
    }
}