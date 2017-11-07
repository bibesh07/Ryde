using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CMPS_285.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated == false)
                return View();
            else
                return View("home");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Feel Free to Contact Us. We are available on Facebook, Twitter and you can also contact us by email.";

            return View();
        }
    }
}