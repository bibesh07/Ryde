using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;

namespace CMPS_285.Models
{
    public class RequestRide
    {
        public int ID { get; set; }
        public string Driver { get; set; }
        public string Passenger { get; set; }

        [Range(1, 4)]
        public int Seats { get; set; }

        [Display(Name = "Tips")]
        [Range(0, 100)]
        public Nullable<int> Cash { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public void emailConfirm(String PassangerName, String DriverName, String email, DateTime Date, String Origin, String Destination)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 25))
            {
                // Configure the client
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("noreplyryde@gmail.com", "CMPS_285");
                // client.UseDefaultCredentials = true;

                // create a MailMessage for ryder
                MailMessage message = new MailMessage(
                                         "noreplyryde@gmail.com", // From field
                                         email, // Recipient field
                                         "Request Accepted", // Subject of the email message
                                         "Hello " + PassangerName + ", <p> Your request for the ride from " + "<strong>" + Destination + "</strong>"+ " to <strong>" +  Origin + "</strong> at time <strong>" + Date + "</strong> has succesfully been accepted by <strong>" + DriverName +
                                         "</strong> </p> <p> <strong> We strongly recommend you to get ready 30 minutes prior to the departure.</strong> </p>" + " <small style='text-align:center;'> Thank you for using our service </small>" // Email message body
                                      );
                // Send the message
                message.IsBodyHtml = true;
                client.Send(message);
            }
        }
    }

    public class RequestRideDBContext : DbContext
    {
        public DbSet<RequestRide> RequestRides { get; set; }
    }
}