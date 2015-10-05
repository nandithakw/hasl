using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Xcendent.Auth.Models.DAL;
using Xcendent.Auth.Models.Entities;

namespace Xcendent.Auth.Models.Managers
{
    public abstract class AbstractXcendentUserManager<TUser>: UserManager<XcendentUser> where TUser : XcendentUser
    {
        public AbstractXcendentUserManager(AbstractXcendentUserStore<XcendentUser, AbstractXcendentAuthContext> store) : base(store)
        {
        }
        public abstract Task<ClaimsIdentity> GenerateUserIdentityAsync(XcendentUser user, string authenticationType);
    }
}