using Owin;
using Xcendant.Auth.Models.DAL;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Cors;
using Xcendant.Auth.Models.Managers;
using Xcendant.Auth.Providers;
using Autofac;
using Xcendant.Auth.Models.Entities;
using Xcendent.Auth.Providers;
using Microsoft.Owin.Security.Facebook;

namespace Xcendant.Auth
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            // app.CreatePerOwinContext(XcendentAuthContext.Create);
            //app.CreatePerOwinContext<XcendentUserManager>(XcendentUserManager.Create);

            app.UseCors(CorsOptions.AllowAll);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            PublicClientId = "self";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new XcendentOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            app.UseOAuthBearerTokens(OAuthOptions);


            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");


            FacebookAuthenticationOptions facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "553074264845816",
                AppSecret = "c71200f8ba3d48f92433c9e1844a7239",
                Provider = new FacebookAuthProvider()
            };
            //  facebookAuthOptions.Scope.Clear();
            facebookAuthOptions.Scope.Add("public_profile");
            facebookAuthOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookAuthOptions);
            GoogleOAuth2AuthenticationOptions googleOptions = new GoogleOAuth2AuthenticationOptions();
            googleOptions.Scope.Clear();
            googleOptions.Scope.Add("profile");
            googleOptions.Scope.Add("email");
            googleOptions.ClientId = "141496314941-4bc07d10tkmctlrcb0ealjp0n45d04dl.apps.googleusercontent.com";
            googleOptions.ClientSecret = "UWQkdq18I3VB7udh3aBsxOK9";
            googleOptions.Provider = new GoogleAuthProvider();
            googleOptions.AccessType = "online";
            //  googleOptions.AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive;

            app.UseGoogleAuthentication(googleOptions);
        }
    }
}