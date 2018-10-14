using BL.Interfaces;
using BL.Repository;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winery.Models;

namespace Winery.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminWineController : Controller
    {
        private WineryDB db;
        private readonly IWineRepository _wineRepository;
        private readonly ISubTypeRepository _subTypeRepository;
        private readonly ITypesRepository _typesRepository;
        private readonly IBottleSize _bottleSizeRepository;
        private readonly IRegion _regionRepository;
      

        public AdminWineController()
        {
            db = new WineryDB();
            _wineRepository = new WineRepository();
            _subTypeRepository = new SubTypeRepository();
            _typesRepository = new TypesRepository();
            _bottleSizeRepository = new BottleSizeRepository();
            _regionRepository = new RegionRepository();
        }
        //GET: AdminWine
        public ActionResult Index()
        {
            return View();
        }
              
        public ActionResult Create()
        {
           var model = new WineViewModel();
           model.TypesList = _typesRepository.GetTypes();
           model.SubTypesList = _subTypeRepository.GetAll();
           model.BottleSizeList = _bottleSizeRepository.GetAll();
           model.RegionList = _regionRepository.GetAll();
           model.CountryList = db.Countries.ToList();
           
            return View(model);
        }

     
        [HttpPost]
        public ActionResult Create(WineViewModel model)
        {
            var validImageTypes = new string[]
   {
        "image/gif",
        "image/jpeg",
        "image/pjpeg",
        "image/png"
   };

                if (model.File == null || model.File.ContentLength == 0)
            {
                ModelState.AddModelError("File", "This field is required");
            }
                else if (!validImageTypes.Contains(model.File.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

          

            if (ModelState.IsValid)
            {              
                var wine = new Wine
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
                    Price = model.Price
                    
                };
        
        if (model.File != null && model.File.ContentLength > 0)
                {
                    var uploadDir = "~/Content/img/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.File.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.File.FileName);
                    model.File.SaveAs(imagePath);
                    wine.ImagePath = imageUrl;
                }
            
                _wineRepository.Insert(wine);
              
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public ActionResult TypesMenuPartial()
        {
            var list = _typesRepository.GetTypes()
                .Select(t => new TypesViewModel
                {
                    TypeID = t.TypeID,
                    TypeName = t.TypeName

                });
            return PartialView(list);
        }

        [ActionName("wines")]
        public ActionResult ListAllWines()
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
                CountryName = t.Countrys.CountryName


            }).ToList();
            return View(wines);
        }

        public ActionResult ListWine(string type)
        {
            var wine = _wineRepository.GetByType(type).Select(t => new WineViewModel
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
                CountryName = t.Countrys.CountryName,
                SubTypes = t.SubTypes.SubTypeName

            }).ToList();
            return View(wine);         
        }

        public ActionResult DeleteWine(int id)
        {
            _wineRepository.Delete(id);
            return RedirectToAction("ListAllWines");
        }

        [ActionName("details")]
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

                Vintage = wine.Vintage,
                Name = wine.Name,
                Description = wine.Description,
                ImagePath = wine.ImagePath,
                BottleSizeID = wine.BottleSizeID,
                Types = wine.Types.TypeName,

                RegionName = wine.Regions.RegionName,
                CountryName = wine.Countrys.CountryName,
                SubTypes = wine.SubTypes.SubTypeName

            };          

            return View(model);
        }


    }
}