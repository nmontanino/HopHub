using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HopHub.Models
{
    public class Beer
    {   
        public int ID { get; set; }

        // ID of beer from BreweryDB API
        public string ReferenceID { get; set; }

        // Average user rating (1-5)
        public double AvgRating { get; set; }

        public int StyleID { get; set; }
        public Style Style { get; set; }

        public int BreweryID { get; set; }
        public Brewery Brewery { get; set; }
    }
}
