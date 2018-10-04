using BL.Interfaces;
using BL.Repository;
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
        private readonly IRegion _regionRepository;
            public AdminRegionCountryController()
        {
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
            return View();
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
    }
}