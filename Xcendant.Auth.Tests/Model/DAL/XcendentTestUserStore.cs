using Xcendant.Auth.Models.DAL;
using Xcendant.Auth.Models.Entities;
namespace Xcendant.Auth.Tests.Model.DAL
{
    class XcendentTestUserStore<TUser, TAuthContext> : AbstractXcendentUserStore<TUser, TAuthContext> where TUser : XcendentUser where TAuthContext : AbstractXcendentAuthContext
    {
        public XcendentTestUserStore(AbstractXcendentAuthContext authContext) : base(authContext)
        {

        }

    }
  
}
