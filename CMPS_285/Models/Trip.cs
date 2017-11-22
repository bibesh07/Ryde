using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

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
        public Nullable<int> pending { get; set; }
        public Nullable<int> confirmed { get; set; }

        public void sendEmailConfirmation(String email , String emailOfDriver, String origin, String destination , String dateTime, String name)
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
                                         "Request Pending", // Subject of the email message
                                         "Hey, You have succesfully requested for a ride. If the ryder accepts your request you will be notified soon.Thank You for your patience!" // Email message body
                                      );
                // Send the message
                client.Send(message);
                //create a MailMessage for driver
                MailMessage messageDriver = new MailMessage(
                                              "noreplyryde@gmail.com", // From field
                                               emailOfDriver, // Recipient field
                                               "New Request to your Hosted ryde (Approval Required)",
                                               "  <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css'>    " +
                                               "<h3> Hola Rider,</h3> <br/> <strong>" + name + "</strong> has requested for reservation of seat for your ride from <strong>" + origin + "</strong> to <strong>" + destination + "</strong> which is scheduled at <strong>" + dateTime + "</strong> <p style='text-align:center;'> Please click either of the buttons to approve or reject the user. You may accept or reject the ryders offer. <br/>" +
                                               " <div style='text-align: center;'> <button type='button' runat='server' id='approve' class='btn btn-success'> Approve </button> &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; <button type='button' runat='server' id='approve' class='btn btn-danger'>Decline</button> </div> "
                                               + "<br/> <footer style='text-align: center;'> Note: <small> Not responsible for cars or personal property lost or stolen or for injury to persons, cars , or personal property or premises. </small> </footer>"); // Subject of the email message  )
                messageDriver.IsBodyHtml = true;
                client.Send(messageDriver);
            }
        }
    }

    public class TripDBContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
    }
}