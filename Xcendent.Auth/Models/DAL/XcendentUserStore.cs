using Xcendent.Auth.Models.Entities;

namespace Xcendent.Auth.Models.DAL
{
    public class XcendentUserStore<TUser, TAuthContext> : AbstractXcendentUserStore<TUser, TAuthContext> where TUser : XcendentUser where TAuthContext : AbstractXcendentAuthContext
    {
        public XcendentUserStore(AbstractXcendentAuthContext authContext) : base(authContext)
        {

        }
    }
}