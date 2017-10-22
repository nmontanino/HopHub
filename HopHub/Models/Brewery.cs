using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class Brewery
    {
        public int ID { get; set; }
        public string BreweryID { get; set; }

        // Will only list beers already in DB. Need to find all beers using API.
        public IList<Beer> Beers { get; set; }

        //public string Name { get; set; }
        //public string Description { get; set; }
        //public string Website { get; set; }

        // Possibly add image URL, and location information (address, latitude, longitude), hours of operation, is open.
    }
}
