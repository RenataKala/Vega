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
    public class WineRepository : IWineRepository
    {
        private WineryDB db;

        public WineRepository()
        {
            db = new WineryDB();
        }

        public void Delete(Wine wine)
        {
            if (wine == null) return;
            db.Wines.Remove(wine);
            db.SaveChanges();
        }

        public void Delete(int wineID)
        {
            Delete(GetByID(wineID));
        }


        public bool Exists(int wineID)
        {
            return db.Wines.Any(t => t.WineID == wineID);
        }

      
        public List<Wine> GetAll()
        {
            return db.Wines.ToList();
        }

        public List<Wine> GetByType(string type)
        {
            return db.Wines.Where(t => t.Types.TypeName == type).ToList();
        }
        public List<Wine> GetByName(string name)
        {
            return db.Wines.Where(t => t.Name == name).ToList();
        }

        public Wine GetByID(int ID)
        {
            return db.Wines.SingleOrDefault(t=>t.WineID == ID);
        }

        public void Insert(Wine wine)
        {
            if (wine == null) return;
            db.Wines.Add(wine);
            db.SaveChanges();
        }

        public void Update(Wine wine)
        {
            if (wine != null && Exists(wine.WineID))
            {
                db.SaveChanges();
            }
        }


    }
}
