using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace CMPS_285.Models
{
        public static class IdentityExtensions
        {
        //FullName
            public static string GetFullName(this IIdentity identity)
            {
                var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
            return (claim != null) ? claim.Value : string.Empty;
            }

        //Email
        public static string GetEmail(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Email");
            return (claim != null) ? claim.Value : string.Empty;
        }

        //PhoneNumber
        public static string GetPhoneNumber(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("PhoneNumber");
            return (claim != null) ? claim.Value : string.Empty;
        }

        //Gender
        public static string GetGender(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Gender");
            return (claim != null) ? claim.Value : string.Empty;
        }

        //StreetAddress
        public static string GetStreetAddress(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("StreetAddress");
            return (claim != null) ? claim.Value : string.Empty;
        }

        //City
        public static string GetCity(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("City");
            return (claim != null) ? claim.Value : string.Empty;
        }

        //State
        public static string GetState(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("State");
            return (claim != null) ? claim.Value : string.Empty;
        }

        //ZipCode
        public static string GetZipCode(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ZipCode");
            return (claim != null) ? claim.Value : string.Empty;
        }

    }

}