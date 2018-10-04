using BL.Interfaces;
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

        public ActionResult AddRegion(RegionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            RegionViewModel region = new RegionViewModel()
            {
                RegionName = model.RegionName
            };
            _regionRepository.Insert(new Region
            {
                RegionName = region.RegionName
            });

            return RedirectToAction("Region","AdminRegionCountry");
        }

        public ActionResult DeleteRegion(int id)
        {
            _regionRepository.Delete(id);
            return RedirectToAction("Region");
        }
        public ActionResult AddCountry(CountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
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

            return RedirectToAction("Country");
        }

        public ActionResult DeleteCountry(int id)
        {
            var country = db.Countries.Any(t => t.CountryID == id);

            return RedirectToAction("Country");
        }
    }
}