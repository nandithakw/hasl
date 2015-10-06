using Microsoft.AspNet.Identity.EntityFramework;
using Xcendant.Auth.Models.Entities;

namespace Xcendant.Auth.Models.DAL
{
    public class AbstractXcendentUserStore<TUser, TAuthContext> : UserStore<TUser> where TUser : XcendentUser where TAuthContext : AbstractXcendentAuthContext
    {
        public AbstractXcendentUserStore(AbstractXcendentAuthContext authContext) : base(authContext)
        {

        }
    }
}