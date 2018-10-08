using BL.Interfaces;
using DL;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository
{
    public class BottleSizeRepository : IBottleSize
    {

        private WineryDB db;

        public BottleSizeRepository()
        {
            db = new WineryDB();
        }

        public void Delete(BottleSize bottlesize)
        {
            if (bottlesize == null) return;
            db.BottleSizes.Remove(bottlesize);
            db.SaveChanges();
        }

        public void Delete(int bottlesizeID)
        {
            Delete(GetByID(bottlesizeID));
        }


        public bool Exists(int bottlesizeID)
        {
            return db.BottleSizes.Any(t => t.BottleSizeID == bottlesizeID);
        }

     

        public List<BottleSize> GetAll()
        {
            return db.BottleSizes.ToList();
        }

        public BottleSize GetByID(int ID)
        {
            return db.BottleSizes.SingleOrDefault(w => w.BottleSizeID == ID);
        }

        public void Insert(BottleSize bottlesize)
        {
            if (bottlesize == null) return;
            db.BottleSizes.Add(bottlesize);
            db.SaveChanges();
        }

        public void Update(BottleSize bottlesize)
        {
            if (bottlesize != null && Exists(bottlesize.BottleSizeID))
            {
                db.SaveChanges();
            }
        }

    }
}
