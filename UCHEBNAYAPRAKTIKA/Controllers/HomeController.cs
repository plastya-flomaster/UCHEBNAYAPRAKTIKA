using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using UCHEBNAYAPRAKTIKA.Models;

namespace UCHEBNAYAPRAKTIKA.Controllers
{
    public class HomeController : Controller
    {
             
        public ActionResult Index()
        {
            return View();
        }

      
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public ViewResult About()
        {
            return View();
        }
        public ViewResult Contact()
        {
            return View();
        }
    }
}