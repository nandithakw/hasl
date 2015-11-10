using Microsoft.Owin.Security.Facebook;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Xcendent.Auth.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        //public override void ApplyRedirect(FacebookApplyRedirectContext context)
        //{
        //    context.Response.Redirect(context.RedirectUri );
        //}
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("external_access_token", context.AccessToken));
            return Task.FromResult<object>(null);
        }
       
        //public override Task ReturnEndpoint(FacebookReturnEndpointContext context)
        //{
        //    return Task.FromResult<object>(null);
        //}
    }
}