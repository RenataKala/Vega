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
    public class CountriesMap : EntityTypeConfiguration<Country>
    {
        public CountriesMap()
        {
            ToTable("Country");

            //Primary Key

            HasKey(w => w.CountryID);
            Property(w => w.CountryID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Properties
            Property(w => w.CountryName).HasMaxLength(50).IsRequired();

        }
    }
}
