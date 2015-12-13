using System.Data.Entity;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess
{
    public class HaslContext : DbContext, IHaslContext
    {
        public HaslContext() : base()
        {

        }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }


    }
}
