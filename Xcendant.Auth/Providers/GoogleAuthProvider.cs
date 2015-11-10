using Microsoft.Owin.Security.Google;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Xcendent.Auth.Providers
{
    public class GoogleAuthProvider : IGoogleOAuth2AuthenticationProvider
    {
        public void ApplyRedirect(GoogleOAuth2ApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri+ "&approval_prompt=auto");
        }

        public Task Authenticated(GoogleOAuth2AuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("external_access_token", context.AccessToken));

            var uri = new Uri(context.User["image"].Value<string>("url"));
            context.Identity.AddClaim(new Claim("picture_url", uri.GetLeftPart(UriPartial.Path)));

            return Task.FromResult<object>(null);
        }

        public Task ReturnEndpoint(GoogleOAuth2ReturnEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }
    }
}