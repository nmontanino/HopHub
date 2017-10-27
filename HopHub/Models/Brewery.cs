using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class Brewery
    {
        // TODO: This will be implemented in the future

        public int ID { get; set; }
        public string BreweryID { get; set; }

        // Will only list beers already in DB. Need to find all beers using API.
        public IList<Beer> Beers { get; set; }

        // Possibly add image URL, and location information (address, latitude, longitude), hours of operation, is open.
    }
}
