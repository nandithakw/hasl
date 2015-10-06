using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Xcendant.Auth.Models.Entities;

namespace Xcendant.Auth.Models.DAL
{
    public abstract class AbstractXcendentAuthContext:IdentityDbContext<XcendentUser>
    {
        public AbstractXcendentAuthContext(string nameOrConnectionString, bool throwIfV1Schema) 
            : base(nameOrConnectionString, throwIfV1Schema)
        {

        }

        public AbstractXcendentAuthContext() {
        }
       
    }
}