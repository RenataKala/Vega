using BL.Interfaces;
using BL.Repository;
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
        //public ActionResult AddWine(WineViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);

        //    }
        //    if (model.File.ContentLength > 0)
        //    {
        //        var uploadedPath = "~/Content/img/uploads/";
        //        var fileName = Path.GetFileName(model.File.FileName);
        //        model.ImagePath = uploadedPath + fileName;

        //        fileName = Path.Combine(Server.MapPath(uploadedPath), fileName);
        //        model.File.SaveAs(fileName);


        //    }

        //    WineViewModel wine = new WineViewModel
        //    {
        //        WineID = model.WineID,
        //        TypeID = model.TypeID,
        //        SubTypeID = model.SubTypeID,
        //        RegionID = model.RegionID,
        //        CountryID = model.CountryID,
        //        BottleSizeID = model.BottleSizeID,
        //        Vintage = model.Vintage,
        //        Name = model.Name,
        //        Description = model.Description,
        //        ImagePath = model.ImagePath,
              
        //    };

        //    _wineRepository.Insert(new Wine
        //    {
        //        WineID = wine.WineID,
        //        TypeID = wine.TypeID,
        //        SubTypeID = wine.SubTypeID,
        //        RegionID = wine.RegionID,
        //        CountryID = wine.CountryID,
        //        BottleSizeID = wine.BottleSizeID,
        //        Vintage = wine.Vintage,
        //        Name = wine.Name,
        //        Description = wine.Description,
        //        ImagePath = wine.ImagePath

        //    });
        //    return View("Index");
        //}
        
        public ActionResult Create()
        {
            return View(new WineViewModel());
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


        //[HttpPost]
        //public string UploadWineImage(HttpPostedFileBase file)
        //{

        //    if (file.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);
        //        var path = Path.Combine(Server.MapPath("~/Content/img/uploads/"), fileName);
        //        file.SaveAs(path);
        //    }

        //    return "";
        //}

    }
}