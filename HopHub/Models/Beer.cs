﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HopHub.Models
{
    public class Beer
    {   
        // Primary Key
        public int ID { get; set; }
        public string Name { get; set; }

        // ID of beer from BreweryDB API
        public string ReferenceID { get; set; }

        // Average user rating (1-5)
        public double? AvgRating { get; set; }

        public IList<Entry> Entries { get; set; }

        //public int StyleID { get; set; }
        //public Style Style { get; set; }

        //public int BreweryID { get; set; }
        //public Brewery Brewery { get; set; }

    }
}
