using CMPS_285.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CMPS_285.Controllers
{
    public class HomeController : Controller
    {
        TripDBContext tripdb = new TripDBContext();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated == false)
                return View();
            else
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var users = db.Users.ToList();
                return View("home", users);
            }
        }

        public ActionResult Users()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult About()
        {
            if (Request.IsAuthenticated == false)
            {
                return View("home");
            }
            else
                return View();
        }

        public ActionResult Message()
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