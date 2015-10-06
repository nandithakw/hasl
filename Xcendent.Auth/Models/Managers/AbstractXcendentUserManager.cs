using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Xcendant.Auth.Models.DAL;
using Xcendant.Auth.Models.Entities;

namespace Xcendant.Auth.Models.Managers
{
    public abstract class AbstractXcendentUserManager<TUser>: UserManager<XcendentUser> where TUser : XcendentUser
    {
        public AbstractXcendentUserManager(AbstractXcendentUserStore<XcendentUser, AbstractXcendentAuthContext> store) : base(store)
        {
        }
        public abstract Task<ClaimsIdentity> GenerateUserIdentityAsync(XcendentUser user, string authenticationType);
    }
}