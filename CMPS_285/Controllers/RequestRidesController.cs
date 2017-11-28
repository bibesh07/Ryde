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
    public class RequestRidesController : Controller
    {
        private RequestRideDBContext db = new RequestRideDBContext();

        // GET: RequestRides
        public ActionResult Index()
        {
            return View(db.RequestRides.ToList());
        }

        // GET: RequestRides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestRide requestRide = db.RequestRides.Find(id);
            if (requestRide == null)
            {
                return HttpNotFound();
            }
            return View(requestRide);
        }

        // GET: RequestRides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestRides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Driver,Passenger,Seats,Cash,Origin,Destination,Time")] RequestRide requestRide)
        {
            if (ModelState.IsValid)
            {
                requestRide.Passenger = User.Identity.GetFullName();
                db.RequestRides.Add(requestRide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requestRide);
        }

        // GET: RequestRides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestRide requestRide = db.RequestRides.Find(id);
            if (requestRide == null)
            {
                return HttpNotFound();
            }
            return View(requestRide);
        }

        // POST: RequestRides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Driver,Passenger,Seats,Cash,Origin,Destination,Time")] RequestRide requestRide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestRide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestRide);
        }

        // GET: RequestRides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestRide requestRide = db.RequestRides.Find(id);
            if (requestRide == null)
            {
                return HttpNotFound();
            }
            return View(requestRide);
        }

        // POST: RequestRides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestRide requestRide = db.RequestRides.Find(id);
            db.RequestRides.Remove(requestRide);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AcceptRequest(string userId, int passengerId, string origin, string destination, DateTime dateTime)
        {
            //GET Driver Name 
            ApplicationDbContext database = new ApplicationDbContext();
            RequestRideDBContext request = new RequestRideDBContext();
            RequestRide Passenger = request.RequestRides.Find(passengerId);

            ApplicationUser user = database.Users.Find(userId);
            String driver = user.Fullname;
            assignDriver(driver, Passenger);

            //GET Passenger name
            String name = Passenger.Passenger;

            //Get passenger email via applicationDBContext
            ApplicationUser P = database.Users.First(c => c.Fullname == name);
            String email = P.Email;
            Passenger.emailConfirm(name,driver,email,dateTime,origin,destination);
            return RedirectToAction("Index");
        }

        protected void assignDriver(String DriverName, RequestRide Trip)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\RequestRides.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connectionString);

            cnn.Open();
            //queries to update in SQL
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql;
            if (Trip.Driver == null)
            {
                sql = "Update RequestRides set Driver = '" + DriverName + "' where ID=" + Trip.ID;
            }
            else
            {
                sql = "Update RequestRides set Driver=" + (" " ) + " where ID=" + Trip.ID;

            }
            command = new SqlCommand(sql, cnn);
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            command.Dispose();
            cnn.Close();
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
