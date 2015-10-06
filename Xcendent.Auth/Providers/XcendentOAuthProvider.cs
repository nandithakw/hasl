﻿using Autofac;
using Autofac.Integration.Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Xcendant.Auth.Models.Entities;
using Xcendant.Auth.Models.Managers;

namespace Xcendant.Auth.Providers
{
    public class XcendentOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientID;
        public XcendentOAuthProvider(string publicClientId)
        {
          
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            this.publicClientID = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ILifetimeScope scope =context.OwinContext.GetAutofacLifetimeScope();
            var userManager = scope.Resolve<AbstractXcendentUserManager<XcendentUser>>();
            XcendentUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await userManager.GenerateUserIdentityAsync(user,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await userManager.GenerateUserIdentityAsync(user,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == publicClientID)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }
            context.Validated();//temp fix
            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "user_name", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}