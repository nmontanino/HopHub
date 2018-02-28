using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class Entry
    {
        public int ID { get; set; }

        public string ApplicationUserID { get; set; } 
        public ApplicationUser ApplicationUser { get; set; }

        public int BeerID { get; set; }
        public Beer Beer { get; set; }

        public int Rating { get; set; }
        public string Review { get; set; }
        public string UserComments { get; set; }
        public string Location { get; set; }
        public DateTime TimeStamp { get; set; }

        // TODO: Implement soft delete
        // public bool IsDeleted { get; set; }
    }
}
