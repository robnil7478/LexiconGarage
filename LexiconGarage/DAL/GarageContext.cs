using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LexiconGarage.DAL
{
    public class GarageContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public GarageContext() : base("name=GarageContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GarageContext>());// XXX Ta bort när färdigt!!!
        }

        public System.Data.Entity.DbSet<LexiconGarage.Models.Vehicle> Vehicles { get; set; }
    }
}
