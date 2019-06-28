using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorsOffice.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Simple description of this application";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you think this is ok, here is my contact. Let's work together!";

            return View();
        }
    }
}