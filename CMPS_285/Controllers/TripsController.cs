using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMPS_285.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;

namespace CMPS_285.Controllers
{
    public class TripsController : Controller
    {
        private TripDBContext db = new TripDBContext();

        // GET: Trips
        public ActionResult Index()
        {
            return View(db.Trips.ToList());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            TripDBContext tripdb = new TripDBContext();
            var user = tripdb.Trips.ToList();
            return View("~/views/home/home.cshtml",user);
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,origin,destination,passanger1,passanger2,passanger3,passanger4,passanger5,time,driver,seats")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.driver = User.Identity.GetFullName();
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trip);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,origin,destination,passanger1,passanger2,passanger3,passanger4,passanger5,time,driver,seats")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
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

        [HttpPost]
        public ActionResult Reserve(string userId, int driverID, string origin, string destination, string dateTime)
        {
            ApplicationDbContext database = new ApplicationDbContext();
            ApplicationUser user = database.Users.Find(userId);
            String email = user.Email;
            String name = user.Fullname;
            Trip trip = db.Trips.Find(driverID);
            String driverName = trip.driver;
            ApplicationUser driver = database.Users.First(c=> c.Fullname == driverName);
            String emailOfDriver = driver.Email;
            seatsPending(trip);
            trip.sendEmailConfirmation(email , emailOfDriver, origin, destination,dateTime, name);
            return RedirectToAction("Index");
        }

        protected void seatsPending(Trip trip)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Trips.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connectionString);

            cnn.Open();
            //queries to update in SQL
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "Update Trips set seats=" + (trip.seats-1) + ", pending= '" + (trip.pending+1) + "' where ID=" + trip.ID ;
            command = new SqlCommand(sql, cnn);
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            command.Dispose();
            cnn.Close();
        }

        public void decider(Trip trip)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Trips.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            if(true)
            {

            }
        }
    }
}
