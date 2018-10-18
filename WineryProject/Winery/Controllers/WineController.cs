using BL.Interfaces;
using BL.Repository;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winery.Models;

namespace Winery.Controllers
{
    public class WineController : Controller

    {
        private WineryDB db;
        private readonly IWineRepository _wineRepository;
        private readonly ITypesRepository _typesRepository;


        public WineController()
        {
            db = new WineryDB();
            _wineRepository = new WineRepository();           
            _typesRepository = new TypesRepository();
          
        }
       // GET: Wine
        public ActionResult GetByType(string type)
        {
            var wines = _wineRepository.GetByType(type).Select(t => new WineViewModel
            {

                CountryID = t.CountryID,
                RegionID = t.RegionID,
                TypeID = t.TypeID,

                Vintage = t.Vintage,
                Name = t.Name,
                Description = t.Description,
                ImagePath = t.ImagePath,
                BottleSizeID = t.BottleSizeID,
                Types = t.Types.TypeName,
                SubTypes = t.SubTypes.SubTypeName,
                Price = t.Price,
                WineID = t.WineID,
                RegionName = t.Regions.RegionName,
                CountryName = t.Countrys.CountryName


            }).ToList();
            return View(wines);
        }

        public ActionResult GetByName(string name)
        {           
            var wines = _wineRepository.GetByName(name).Select(t => new WineViewModel           
            {
                CountryID = t.CountryID,
                RegionID = t.RegionID,
                TypeID = t.TypeID,
                SubTypeID=t.SubTypeID,
                WineID = t.WineID,
                Vintage = t.Vintage,
                Name = t.Name,
                Description = t.Description,
                ImagePath = t.ImagePath,
                BottleSizeID = t.BottleSizeID,
                Types = t.Types.TypeName,
                SubTypes = t.SubTypes.SubTypeName,
                Price = t.Price,
                RegionName = t.Regions.RegionName,
                CountryName = t.Countrys.CountryName
            }).ToList();
            if (wines == null)
            {
                return Content("This wine does not exist");
            }
            return View(wines);
        }
        
        [ActionName("all-wines")]
        public ActionResult AllWines()
        {
            var wines = _wineRepository.GetAll().Select(t => new WineViewModel
            {
                WineID = t.WineID,
                CountryID = t.CountryID,
                RegionID = t.RegionID,
                TypeID = t.TypeID,
                SubTypeID = t.SubTypeID,
                Vintage = t.Vintage,
                Name = t.Name,
                Description = t.Description,
                ImagePath = t.ImagePath,
                BottleSizeID = t.BottleSizeID,
                Types = t.Types.TypeName,
                SubTypes = t.SubTypes.SubTypeName,
                RegionName = t.Regions.RegionName,
                CountryName = t.Countrys.CountryName,
                Price= t.Price


            }).ToList();
            return View(wines);
        }
        public ActionResult Details(int id)
        {
            var wine = _wineRepository.GetByID(id);
            if (wine == null)
            {
                return Content("This wine does not exist");
            }
            WineViewModel model = new WineViewModel()
            {
                CountryID = wine.CountryID,
                RegionID = wine.RegionID,
                TypeID = wine.TypeID,
                WineID = wine.WineID,

                Vintage = wine.Vintage,
                Name = wine.Name,
                Description = wine.Description,
                ImagePath = wine.ImagePath,
                BottleSizeID = wine.BottleSizeID,
                Types = wine.Types.TypeName,
                Price = wine.Price,

                RegionName = wine.Regions.RegionName,
                CountryName = wine.Countrys.CountryName,
                SubTypes = wine.SubTypes.SubTypeName

            };

            return View(model);
        }

        public ActionResult GetByTypePartial(string type)
        {
            var wines = _wineRepository.GetByType(type).Select(t => new WineViewModel
            {

                CountryID = t.CountryID,
                RegionID = t.RegionID,
                TypeID = t.TypeID,

                Vintage = t.Vintage,
                Name = t.Name,
                Description = t.Description,
                ImagePath = t.ImagePath,
                BottleSizeID = t.BottleSizeID,
                Types = t.Types.TypeName,
                Price= t.Price,

                RegionName = t.Regions.RegionName,
                CountryName = t.Countrys.CountryName


            }).ToList();
            return PartialView(wines);

        }
        

     
    }
}
