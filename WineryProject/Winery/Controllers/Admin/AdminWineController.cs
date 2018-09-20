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
    public class AdminWineController : Controller
    {
        private readonly IWineRepository _wineRepository;

        public AdminWineController()
        {
            _wineRepository = new WineRepository();
        }
        //GET: AdminWine
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddWine(WineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            WineViewModel wine = new WineViewModel
            {
                WineID = model.WineID,
                TypeID = model.TypeID,
                SubTypeID = model.SubTypeID,
                RegionID = model.RegionID,
                CountryID = model.CountryID,
                BottleSizeID = model.BottleSizeID,
                Vintage = model.Vintage,
                Name = model.Name,
                Description = model.Description,
                ImagePath = model.ImagePath
            };

            _wineRepository.Insert(new Wine
            {
                WineID = wine.WineID,
                TypeID = wine.TypeID,
                SubTypeID = wine.SubTypeID,
                RegionID = wine.RegionID,
                CountryID = wine.CountryID,
                BottleSizeID = wine.BottleSizeID,
                Vintage = wine.Vintage,
                Name = wine.Name,
                Description = wine.Description,
                ImagePath = wine.ImagePath

            });
            return View("Index");
        }

    }
}