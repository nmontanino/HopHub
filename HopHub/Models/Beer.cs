using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class Beer
    {
        // TODO: Finish Beer model class

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double ABV { get; set; }

        public string LogoLargeURL { get; set; }
        public string LogoMedURL { get; set; }

        // Average user rating (1-5)
        public double AvgRating { get; set; }

        public int StyleID { get; set; }
        public Style Style { get; set; }

        public int BreweryID { get; set; }
        public Brewery Brewery { get; set; }
    }
}
