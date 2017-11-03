using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class AddEntryViewModel
    {   
        public string BeerID { get; set; }
        public string BeerName { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        [Display(Name = "Add a review for this beer")]
        public string Review { get; set; }

        [Display(Name = "Additional notes or comments")]
        public string UserComments { get; set; }

        [Display(Name = "Purchase location")]
        public string Location { get; set; }
    }
}
