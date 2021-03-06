﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public class Wine
    {
        public int WineID { get; set; }
        public int TypeID { get; set; }
        public int RegionID { get; set; }
        public int CountryID { get; set; }
        public int BottleSizeID { get; set; }
        public int SubTypeID { get; set; }
        public int Vintage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }

        //mapped
        public virtual Types Types { get; set; }
        //mapped
        public virtual SubType SubTypes { get; set; }
        //mapped
        public virtual Country Countrys { get; set; }
        //mapped
        public virtual Region Regions { get; set; }
        //mapped
        public virtual BottleSize BottleSizes { get; set; }
    }
}
