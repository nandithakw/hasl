using Microsoft.AspNet.Identity.EntityFramework;
using Xcendent.Auth.Models.Entities;

namespace Xcendent.Auth.Models.DAL
{
    public class AbstractXcendentUserStore<TUser, TAuthContext> : UserStore<TUser> where TUser : XcendentUser where TAuthContext : AbstractXcendentAuthContext
    {
        public AbstractXcendentUserStore(AbstractXcendentAuthContext authContext) : base(authContext)
        {

        }
    }
}