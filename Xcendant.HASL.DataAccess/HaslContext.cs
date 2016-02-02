using System.Data.Entity;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess
{
    public class HaslContext : DbContext, IHaslContext
    {
        public HaslContext() : base()
        {
            base.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<CareGiver> CareGivers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<TreatmentCenter> TreatmentCenters { get; set; }
        DbSet<Patient> Patients { get; set; }



    }
}
