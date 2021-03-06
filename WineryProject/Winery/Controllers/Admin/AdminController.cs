﻿using BL.Interfaces;
using BL.Repository;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winery.Models;


namespace Winery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IWineRepository _wineRepository;
        private readonly ISubTypeRepository _subTypeRepository;
        private readonly ITypesRepository _typeRepository;
        private readonly IBottleSize _bottleSizeRepository;
        public AdminController()
        {
            _wineRepository = new WineRepository();
            _subTypeRepository = new SubTypeRepository();
            _typeRepository = new TypesRepository();
            _bottleSizeRepository = new BottleSizeRepository();
        }
        // GET: Admin
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult GetAllTypes()
        {
            var types = _typeRepository.GetTypes()
                .Select(t => new TypesViewModel
                {
                    TypeID = t.TypeID,
                    TypeName = t.TypeName
                }).ToList();
            return View("GetAllTypes", types);
        }
     
        public ActionResult GetAllSubTypes()
        {
            var types = _subTypeRepository.GetAll()
                .Select(t => new SubTypeViewModel
                {
                    SubTypeID = t.SubTypeID,
                    SubTypeName = t.SubTypeName
                }).ToList();
            return View("GetAllSubTypes", types);
        }

        [HttpPost]
        public string AddNewType(string catName)
        {
            string id;
            if(_typeRepository.GetTypes().Any(t=>t.TypeName == catName))
            {
                return "titletaken";
            }
            Types types = new Types();
            types.TypeName = catName;

           _typeRepository.Insert(types);

            id = types.TypeID.ToString();
            return id;
        }

        [HttpPost]
        public string AddNewSubType(string catName)
        {
            string id;
            if (_subTypeRepository.GetAll().Any(t => t.SubTypeName == catName))
            {
                return "titletaken";
            }
            SubType subtypes = new SubType();
            subtypes.SubTypeName = catName;

            _subTypeRepository.Insert(subtypes);

            id = subtypes.SubTypeID.ToString();
            return id;
        }

        public ActionResult DeleteType(int id)
        {
            _typeRepository.Delete(id);

            return RedirectToAction("GetAllTypes"); 
        }

        public ActionResult DeleteSubType(int id)
        {
            _subTypeRepository.Delete(id);

            return RedirectToAction("GetAllSubTypes");
        }

        [HttpGet]
        public ActionResult BottleSize()
        {
            var sizes = _bottleSizeRepository.GetAll()
                .Select(t=> new BottleSizeViewModel {
                    BottleSizeID = t.BottleSizeID,
                    Size = t.Size
                }).ToList();

            return View(sizes);
        }


        [HttpPost]
        public string AddNewSize(string sizeNum)
        {
            string id;
            if (_bottleSizeRepository.GetAll().Any(t => t.Size == sizeNum))
            {
                return "This Bottle Size already exists";
            }
            BottleSize bs = new BottleSize();
            bs.Size =  sizeNum;

            _bottleSizeRepository.Insert(bs);

            id = bs.BottleSizeID.ToString();
            return id;
        }
        public ActionResult DeleteBottleSize(int id)
        {
            _bottleSizeRepository.Delete(id);

            return RedirectToAction("BottleSize");

        }

        public string RenameType( string NewCatName, int id)
        {
            if(_typeRepository.GetTypes().Any(t=>t.TypeName == NewCatName))
            {
                return "Type name already exists";
            }
            Types type = _typeRepository.GetByID(id);
            type.TypeName = NewCatName;
            _typeRepository.Update(type);

            return "";         

        }

        public int  CountAll()       
        {           
            var count = _wineRepository.GetAll().Count();
            
            return count;
        }
        public int CountWhite()
        {
            var count = _wineRepository.GetAll().Where(t => t.TypeID == 48).Count();
            
            return count;
        }

        public int CountWine(string type)
        {
            var count = _wineRepository.GetByType(type).Count();
            return count;
        }
        public ActionResult DeleteWine(int id)
        {
            _wineRepository.Delete(id);
            return RedirectToAction("ListAllWines");
        }



    }
}