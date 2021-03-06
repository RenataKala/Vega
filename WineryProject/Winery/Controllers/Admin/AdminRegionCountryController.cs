﻿using BL.Interfaces;
using BL.Repository;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winery.Models;

namespace Winery.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminRegionCountryController : Controller
    {
        private WineryDB db;
        private readonly IRegion _regionRepository;

            public AdminRegionCountryController()
        {
            db = new WineryDB();
            _regionRepository = new RegionRepository();
        }
        // GET: AdminRegionCountry
        public ActionResult Region()
        {
            var regions = _regionRepository.GetAll()
                .Select(t => new RegionViewModel
                {
                    RegionID = t.RegionID,
                    RegionName = t.RegionName

                }).ToList();
            return View(regions);
        }

        public ActionResult Country()
        {
            var countries = db.Countries.Select(t => new CountryViewModel
            {
                CountryID = t.CountryID,
                CountryName = t.CountryName
            }).ToList();
            return View(countries);
        }

        public string AddRegion(RegionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return "Must enter a region name";
            }
                if (_regionRepository.GetAll().Any(t=>t.RegionName == model.RegionName))
            {
                return "This region is already on the list";
            }
            RegionViewModel region = new RegionViewModel()
            {
                RegionID = model.RegionID,
                RegionName = model.RegionName
            };
            _regionRepository.Insert(new Region
            {
                RegionID = region.RegionID,
                RegionName = region.RegionName
            });

            return "";
        }

        public ActionResult DeleteRegion(int id)
        {
            _regionRepository.Delete(id);
            return RedirectToAction("Region");
        }
        public string AddCountry(CountryViewModel model)
        {
           
            if(db.Countries.Any(t=>t.CountryName == model.CountryName))
            {
                return "This country is already on the list";
            }
            CountryViewModel country = new CountryViewModel()
            {
                CountryID = model.CountryID,
                CountryName = model.CountryName
            };
            db.Countries.Add(new Country()
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            });
            db.SaveChanges();

            return "";
        }

        public ActionResult DeleteCountry(int id)
        {
            var country = db.Countries.Find(id);
            db.Countries.Remove(country);
            db.SaveChanges();

            return RedirectToAction("Country");
        }
    }
}