using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class AddEntryViewModel
    {   
        [Required]
        public string BeerID { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        public string Review { get; set; }
        public string UserComments { get; set; }
        public string Location { get; set; }

        //public DateTime TimeStamp { get; set; }
    }
}
