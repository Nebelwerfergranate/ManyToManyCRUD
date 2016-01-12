using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Motorists.Models
{
    public class MotoristContext : DbContext
    {
        static MotoristContext()
        {
            //System.Data.Entity.Database.SetInitializer(new MotoristContextInitializer());    
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>()
                .HasMany(o => o.Cars)
                .WithMany(c => c.Owners);
            base.OnModelCreating(modelBuilder);
        }
    }
}