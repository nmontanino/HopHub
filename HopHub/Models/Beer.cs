﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HopHub.Models
{
    public class Beer
    {   
        public int ID { get; set; }
        public string Name { get; set; }

        // ID of beer from BreweryDB API
        public string ReferenceID { get; set; }

        // Average user rating
        public double? AvgRating { get; set; }

        public IList<Entry> Entries { get; set; }

        //public override string ToString()
        //{
        //    if (AvgRating == 1)
        //    {
        //        return "★";
        //    }
        //    return "This beer has not yet been rated";
        //}
    }
}
