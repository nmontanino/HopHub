using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class EditEntryViewModel : AddEntryViewModel
    {
        public int ID { get; set; }

        public static EditEntryViewModel EditEntry(Entry entry)
        {
            EditEntryViewModel edit = new EditEntryViewModel
            {
                ID = entry.ID,
                BeerName = entry.Beer.Name,
                Rating = entry.Rating,
                Review = entry.Review,
                UserComments = entry.UserComments,
                Location = entry.Location
            };

            return edit;
        }
    }
}
