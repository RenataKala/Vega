using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Maps
{
    public class WineMap : EntityTypeConfiguration<Wine>
    {
        public WineMap()
        {
            ToTable("Wine");
            //Primary Key

            HasKey(w => w.WineID);
            Property(w => w.WineID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Properties
            Property(w => w.TypeID).IsRequired();
            Property(w => w.Name).HasMaxLength(15).IsRequired();
            Property(w => w.RegionID).IsRequired();
            Property(w => w.CountryID).IsRequired();
            Property(w => w.BottleSizeID).IsRequired();
            Property(w => w.ImagePath).HasMaxLength(128).IsRequired();
            Property(w => w.Description).HasMaxLength(4096).IsRequired();
            Property(w => w.Vintage).IsRequired();
            Property(w => w.SubTypeID).IsRequired();
        }
    }
}
