using System.Security.Claims;

namespace Xcendent.Auth.Models.Managers
{
    public class XcendantAuthorizationManager : ClaimsAuthorizationManager
    {

        public override bool CheckAccess(AuthorizationContext context)
        {
            var identity = context.Principal.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                
                return false;
            }

            return true;

        }
    }
}