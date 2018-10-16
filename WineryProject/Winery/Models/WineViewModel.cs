using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Winery.Models
{
    public class WineViewModel
    {
        public WineViewModel()
        {

        }
        public WineViewModel(Wine wine)
        {
            WineID = wine.WineID;
            TypeID = wine.TypeID;
            RegionID = wine.RegionID;
            CountryID = wine.CountryID;
            BottleSizeID = wine.BottleSizeID;
            SubTypeID = wine.SubTypeID;
            Vintage = wine.Vintage;
            Name = wine.Name;
            Description = wine.Description;
            ImagePath = wine.ImagePath;
                       
        }


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
        [DisplayName("Upload image")]
        public HttpPostedFileBase File { get; set; }

       
        public string Types { get; set; }
        public string RegionName { get; set; }
        public string CountryName { get; set; }
        public string SubTypes { get; set; }

        public List<Types> TypesList { get; set; }
        public List<Region> RegionList { get; set; }
        public List<Country> CountryList { get; set; }
        public List<SubType> SubTypesList { get; set; }
        public List<BottleSize> BottleSizeList { get; set; }

    }
}