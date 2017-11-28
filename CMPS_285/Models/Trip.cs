using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace CMPS_285.Models
{
    public class Trip
    {
        public int ID { get; set; }
        [Required]
        public string origin { get; set; }
        [Required]
        public string destination { get; set; }
        public string passanger1 { get; set; }
        public string passanger2 { get; set; }
        public string passanger3 { get; set; }
        public string passanger4 { get; set; }
        public string passanger5 { get; set; }
        [Required]
        public DateTime time { get; set; }
        public string driver { get; set; }

        [Range(1, 5)]
        public int seats { get; set; }


        public int pending { get; set; }
        public int confirmed { get; set; }
        public String p1Status { get; set; }
        public String p2Status { get; set; }
        public String p3Status { get; set; }
        public String p4Status { get; set; }
        public String p5Status { get; set; }


        public void sendEmailConfirmation(String email , String emailOfDriver, String origin, String destination , DateTime dateTime, String name)
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
                                               " <div style='text-align: center;'>" +
                                               "" +
                                               " <a href='google.com' ><button type='submit' runat='server' id='approve'> Approve </button></a> &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; <button type='submit' PostBackUrl='google.com' runat='server' id='decline'>Decline</button> </div> "
                                               + "" +
                                               "<br/> <footer style='text-align: center;'> Note: <small> Not responsible for cars or personal property lost or stolen or for injury to persons, cars , or personal property or premises. </small> </footer>"); // Subject of the email message  )
                messageDriver.IsBodyHtml = true;
                client.Send(messageDriver);
            }
        }

        public void sendConfirmed(String email, String origin, String destination, DateTime dateTime)
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
                                         "Hello, </br> Your request for the ride from" + destination + " to " + origin +" at time " + dateTime +" has succesfully been accepted by the ryder  " +
                                         "</br> <strong> We strongly recommend you to get ready 30 minutes prior to the departure.</strong>"+ " <small> Thank you for using our service </small>" // Email message body
                                      );
                // Send the message
                message.IsBodyHtml = true;
                client.Send(message);
            }
        }

        public void refer(String email)
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
                                         "Join us ", // Subject of the email message
                                         "Hello, </br> Your friend  wants you to join Ryde. </br> Join today and start getting free rides. Click this link to continue <a href=''> Click me </a> ");
                // Send the message
                message.IsBodyHtml = true;
                client.Send(message);
            }
        }

        public void FeedBack(String name, String email, String Subject, String Message)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 25))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("noreplyryde@gmail.com", "CMPS_285");
                MailMessage message = new MailMessage(
                                        "noreplyryde@gmail.com",
                                        "pratikshya.timalsina@selu.edu",
                                        Subject,
                                        "Hi Mr President," + "<br/>" + "<p>" +
                                        "This message is sent by " + name + "</p>" + "with the email adress" + email + "<p>" + Message + "</p>");
                message.IsBodyHtml = true;
                client.Send(message);
            }

        }


    }
    public class TripDBContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
    }
}