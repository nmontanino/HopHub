using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class Entry
    {
        public int ID { get; set; }

        // NOTE: ID for ApplicationUser uses string for primary key
        public string ApplicationUserID { get; set; } 
        public ApplicationUser ApplicationUser { get; set; }


        //TODO: Fix these properties.
        //public int BeerID { get; set; }
        //public Beer Beer { get; set; }

        public string BeerID { get; set; }

        public int Rating { get; set; }
        public string Review { get; set; }
        public string UserComments { get; set; }
        public string Location { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
