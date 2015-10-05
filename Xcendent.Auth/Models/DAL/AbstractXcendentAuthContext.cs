using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Xcendent.Auth.Models.Entities;

namespace Xcendent.Auth.Models.DAL
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