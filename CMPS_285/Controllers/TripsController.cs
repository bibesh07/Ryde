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
        public ActionResult Reserve(string userId, int driverID, string origin, string destination, DateTime dateTime)
        {
            ApplicationDbContext database = new ApplicationDbContext();
            ApplicationUser user = database.Users.Find(userId);
            String email = user.Email;
            String name = user.Fullname;
            Trip trip = db.Trips.Find(driverID);
            String driverName = trip.driver;
            ApplicationUser driver = database.Users.First(c=> c.Fullname == driverName);
            String emailOfDriver = driver.Email;
            seatsPending(trip, name);
            trip.sendEmailConfirmation(email , emailOfDriver, origin, destination,dateTime, name);
            //temp notificaitons
            TempData["Message"] = "You have succesfully requested your ride. Your ride request is pending you will be notified once your ride is confirmed. Thank You";
            return RedirectToAction("Index");
        }

        protected void seatsPending(Trip trip, String name)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Trips.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connectionString);

            cnn.Open();
            //queries to update in SQL
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql;
            if (trip.passanger1 == null)
            {
                sql = "Update Trips set seats=" + (trip.seats - 1) + ", pending= '" + (++trip.pending) + "', passanger1 = '" + name + "', p1Status ='" + "P" + "'where ID=" + trip.ID;
            }
            else if (trip.passanger2 == null)
            {
                sql = "Update Trips set seats=" + (trip.seats - 1) + ", pending= '" + (++trip.pending) + "', passanger2 = '" + name + "', p2Status ='" + "P" + "'where ID=" + trip.ID;
            }
            else if (trip.passanger3 == null)
            {
                sql = "Update Trips set seats=" + (trip.seats - 1) + ", pending= '" + (trip.pending + 1) + "', passanger3 = '" + name + "', p3Status ='" + "P" + "'where ID=" + trip.ID;
            }
            else if (trip.passanger4 == null)
            {
                sql = "Update Trips set seats=" + (trip.seats - 1) + ", pending= '" + (trip.pending + 1) + "', passanger4 = '" + name + "', p4Status ='" + "P" + "'where ID=" + trip.ID;
            }
            else
            {
                sql = "Update Trips set seats=" + (trip.seats - 1) + ", pending= '" + (trip.pending + 1) + "', passanger5 = '" + name + "', p5Status ='" + "P" + "'where ID=" + trip.ID;
            }
            command = new SqlCommand(sql, cnn);
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            command.Dispose();
            cnn.Close();
        }

        public ActionResult Decider(int driverId, Boolean decision, String name)
        {
            Trip trip = db.Trips.Find(driverId);
            ApplicationDbContext database = new ApplicationDbContext();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Trips.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand command;
            String sql;
            SqlDataAdapter adapter = new SqlDataAdapter();
            cnn.Open();
            if (decision == true && trip.passanger1 == name && trip.p1Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", confirmed= '" + (++trip.confirmed) + "', p1Status ='"+ "C" + "'where ID=" + trip.ID;
                ApplicationUser passanger = database.Users.First(c => c.Fullname == trip.passanger1);
                String email = passanger.Email;
                trip.sendConfirmed(email, trip.origin, trip.destination, trip.time);
            }
            else if (decision == true && trip.passanger2 == name && trip.p2Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", confirmed= '" + (++trip.confirmed) + "', p2Status ='" + "C" + "'where ID=" + trip.ID;
                ApplicationUser passanger = database.Users.First(c => c.Fullname == trip.passanger2);
                String email = passanger.Email;
                trip.sendConfirmed(email, trip.origin, trip.destination, trip.time);
            }
            else if (decision == true && trip.passanger3 == name && trip.p3Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", confirmed= '" + (++trip.confirmed) + "', p3Status ='" + "C" + "'where ID=" + trip.ID;
                ApplicationUser passanger = database.Users.First(c => c.Fullname == trip.passanger3);
                String email = passanger.Email;
                trip.sendConfirmed(email, trip.origin, trip.destination, trip.time);

            }
            else if (decision == true && trip.passanger4 == name && trip.p4Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", confirmed= '" + (++trip.confirmed) + "', p4Status ='" + "C" + "'where ID=" + trip.ID;
                ApplicationUser passanger = database.Users.First(c => c.Fullname == trip.passanger4);
                String email = passanger.Email;
                trip.sendConfirmed(email, trip.origin, trip.destination, trip.time);
            }
            else if (decision == true && trip.passanger5 == name && trip.p5Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", confirmed= '" + (++trip.confirmed) + "', p5Status ='" + "C" + "'where ID=" + trip.ID;
                ApplicationUser passanger = database.Users.First(c => c.Fullname == trip.passanger5);
                String email = passanger.Email;
                trip.sendConfirmed(email, trip.origin, trip.destination, trip.time);
            }
            else if(decision == false && trip.passanger1 == name && trip.p1Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", seats= '" + (++trip.seats) + "', passanger1 ='"+ " " +"', p1Status ='" + " " + "'where ID=" + trip.ID;
            }
            else if (decision == false && trip.passanger2 == name && trip.p2Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", seats= '" + (++trip.seats) + "', passanger2 ='" + " " + "', p2Status ='" + " " + "'where ID=" + trip.ID;
            }
            else if (decision == false && trip.passanger3 == name && trip.p3Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", seats= '" + (++trip.seats) + "', passanger3 ='" + " " + "', p3Status ='" + " " + "'where ID=" + trip.ID;
            }
            else if (decision == false && trip.passanger4 == name && trip.p4Status == "P")
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", seats= '" + (++trip.seats) + "', passanger4 ='" + " " + "', p4Status ='" + " " + "'where ID=" + trip.ID;
            }
            else 
            {
                sql = "Update Trips set pending=" + (--trip.pending) + ", seats= '" + (++trip.seats) + "', passanger5 ='" + " " + "', p5Status ='" + " " + "'where ID=" + trip.ID;
            }
            command = new SqlCommand(sql, cnn);
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            command.Dispose();
            cnn.Close();
            return RedirectToAction("requestedRides");
        }

        public ActionResult referaFriend()
        {
            return View();
        }

        [HttpPost]
        public ActionResult referralConfirmation(String email)
        {
            ApplicationDbContext data = new ApplicationDbContext();
            foreach (var item in data.Users)
            {
                if( item.Email == email)
                {
                    return View("ReferaFriendDenied");
                }
            }
            Trip trip = new Trip();
            trip.refer(email);
            TempData["Message"] = "You have succesfully requested your friend Thank You. If you want to request any more of your friend feel free.";
            return RedirectToAction("referaFriend");
        }

        public ActionResult requestedRides()
        {
            return View(db.Trips.ToList());
        }
    }
}
