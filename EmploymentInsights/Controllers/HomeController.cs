using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmploymentInsights.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            PeriodicGetter getter = new PeriodicGetter();
            getter.interval = 1;
            getter.Start();

            return View();
        }
    }
}
