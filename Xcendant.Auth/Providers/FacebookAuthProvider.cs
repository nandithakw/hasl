using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Facebook;
using Newtonsoft.Json.Linq;

namespace Xcendent.Auth.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        public override void ApplyRedirect(FacebookApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri);
        }
        public override async Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("external_access_token", context.AccessToken));
            JObject userDtls = await new FaceBookUserDetailsProvider().GetUserInfo(
                new System.Collections.Generic.Dictionary<string, string>()
                {
                    ["access_token"] = context.AccessToken,
                    ["user_id"] = context.Id,

                });

            var uri = (userDtls["picture"] as JObject).GetValue("url").Value<string>();
            context.Identity.AddClaim(new Claim("picture_url", uri));
            var email = userDtls.GetValue("email").Value<String>();
            context.Identity.AddClaim(new Claim(ClaimTypes.Email, email));


            // return Task.FromResult(null);
        }

        public override Task ReturnEndpoint(FacebookReturnEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }
    }
}