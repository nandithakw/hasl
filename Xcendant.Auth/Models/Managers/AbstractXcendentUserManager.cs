using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xcendant.Auth.Models.DAL;
using Xcendant.Auth.Models.Entities;

namespace Xcendant.Auth.Models.Managers
{
    public abstract class AbstractXcendentUserManager<TUser> : UserManager<XcendentUser> where TUser : XcendentUser
    {
        public AbstractXcendentUserManager(AbstractXcendentUserStore<XcendentUser, AbstractXcendentAuthContext> store, IdentityFactoryOptions<AbstractXcendentUserManager<XcendentUser>> options) : base(store)
        {
        }

        public AbstractXcendentUserManager(IUserStore<XcendentUser> store, IdentityFactoryOptions<AbstractXcendentUserManager<XcendentUser>> options) : base(store)
        {
        }

        public abstract Task<ClaimsIdentity> GenerateUserIdentityAsync(XcendentUser user, string authenticationType, IList<Claim> extraClaims);
    }
}