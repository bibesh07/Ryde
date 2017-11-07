using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CMPS_285.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public String Fullname { get; set; }
        public String Password { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String ZipCode { get; set; }
        public String StreetAddress { get; set; }
        public String PhoneNumber { get; set; }
        public String City { get; set; }
        public String State { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //Fullname
            userIdentity.AddClaim(new Claim("Fullname", this.Fullname));
            //Email
            userIdentity.AddClaim(new Claim("Email", this.Email));

            //PhoneNumber
            userIdentity.AddClaim(new Claim("PhoneNumber", this.PhoneNumber));

            //Gender
            userIdentity.AddClaim(new Claim("Gender", this.Gender));

            //StreetAddress
            userIdentity.AddClaim(new Claim("StreetAddress", this.StreetAddress));

            //City
            userIdentity.AddClaim(new Claim("City", this.City));

            //State
            userIdentity.AddClaim(new Claim("State", this.State));

            //ZipCode
            userIdentity.AddClaim(new Claim("ZipCode", this.ZipCode));

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}