using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Winery.Models
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {

        }
        public AdminViewModel(WineViewModel Wine, TypesViewModel Types, SubTypeViewModel Subtype, RegionViewModel Region, CountryViewModel Country, BottleSizeViewModel BottleSize)
        {
            Wine.Name = "Wine";
            Types.TypeName = "Types";

        }
        public WineViewModel Wine { get; set; }
        public TypesViewModel Types { get; set; }
        public SubTypeViewModel Subtype { get; set; }
        public RegionViewModel Region { get; set; }
        public CountryViewModel Country { get; set; }
        public BottleSizeViewModel BottleSize { get; set; }
    }
}