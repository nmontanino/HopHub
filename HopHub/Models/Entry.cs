using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class Entry
    {
        public int ID { get; set; }

        public int ApplicationUserID { get; set; } // TODO: Change ID to string in User table
        public ApplicationUser ApplicationUser { get; set; }

        public int BeerID { get; set; }
        public Beer Beer { get; set; }

        public int Rating { get; set; }
        public string Review { get; set; }

        public string UserComments { get; set; }
        public string Location { get; set; }

        // TODO: Datetime to be set at creation of each entry.
        public DateTime EntryDateTime { get; set; }
    }
}
