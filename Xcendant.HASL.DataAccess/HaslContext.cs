﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess
{
    public class HaslContext : DbContext, IHaslContext
    {
        public HaslContext() : base()
        {
            base.Configuration.LazyLoadingEnabled = false;

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<CareGiver> CareGivers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<TreatmentCenter> TreatmentCenters { get; set; }



    }
}
