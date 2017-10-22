using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HopHub.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // Must be at least 21 years old to sign up for app because legal reasons
        public bool Age { get; set; }

        IList<Entry> Entries { get; set; }
    }
}
