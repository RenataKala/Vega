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

        //GET: Wine/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: Wine/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wine/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Wine/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wine/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Wine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
