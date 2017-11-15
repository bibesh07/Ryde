using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CMPS_285.Models
{
    public class Trip
    {
        public int ID { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string passanger1 { get; set; }
        public string passanger2 { get; set; }
        public string passanger3 { get; set; }
        public string passanger4 { get; set; }
        public string passanger5 { get; set; }
        public string time { get; set; }
        public string driver { get; set; }
        public int seats { get; set; }

        public void seatOccupied()
        {
            seats--;
        }
    }

    public class TripDBContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
    }
}