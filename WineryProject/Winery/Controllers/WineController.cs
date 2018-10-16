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

                Vintage = t.Vintage,
                Name = t.Name,
                Description = t.Description,
                ImagePath = t.ImagePath,
                BottleSizeID = t.BottleSizeID,
                Types = t.Types.TypeName,

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
            var list = _typesRepository.GetTypes()
                .Select(t => new TypesViewModel
                {
                    TypeID = t.TypeID,
                    TypeName = t.TypeName

                });
            return View(list);
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
