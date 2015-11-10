using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Xcendant.Auth.Models.Entities;

namespace Xcendent.Auth.ViewModels
{
    public class LoginData
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ExternalAccesToken { get; set; }
        public string PictureUrl { get; set; }
        public IList<Claim> GetClaims()
        {
            IList<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));
            if (Name != null) { claims.Add(new Claim(ClaimTypes.Name, Name, null, LoginProvider)); }
            if (Email != null) { claims.Add(new Claim(ClaimTypes.Email, Email, null, LoginProvider)); }
            if (ExternalAccesToken != null) { claims.Add(new Claim("external_access_token", ExternalAccesToken, null, ExternalAccesToken)); }
            if (PictureUrl != null) { claims.Add(new Claim("picture_url", PictureUrl, null, PictureUrl)); }
            return claims;
        }
        public static IList<Claim> GetClaimsFromUserForLocalAuthority(XcendentUser user)
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim("picture_url", "#"));
            //  claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));
            //if (user.Email != null) { claims.Add(new Claim(ClaimTypes.Name, user.Email)); }
            if (user.Email != null) { claims.Add(new Claim(ClaimTypes.Email, user.Email)); }
            //if (ExternalAccesToken != null) { claims.Add(new Claim("external_access_token", ExternalAccesToken, null, ExternalAccesToken)); }
            return claims;

        }
        public static IList<Claim> GetClaimsFromExternalIdentityForLocalAuthority(LoginData externalLoginData)
        {
            IList<Claim> claims = new List<Claim>();
            if (externalLoginData.Name != null) { claims.Add(new Claim(ClaimTypes.Name, externalLoginData.Name)); }
            if (externalLoginData.Email != null) { claims.Add(new Claim(ClaimTypes.Email, externalLoginData.Email)); }
            if (externalLoginData.ExternalAccesToken != null) { claims.Add(new Claim("external_access_token", externalLoginData.ExternalAccesToken)); }
            if (externalLoginData.PictureUrl != null) { claims.Add(new Claim("picture_url", externalLoginData.PictureUrl)); }
            return claims;

        }
        public static LoginData FromIdentity(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                || String.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            return new LoginData
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                Name = identity.FindFirstValue(ClaimTypes.Name),
                Email = identity.FindFirstValue(ClaimTypes.Email),
                ExternalAccesToken = identity.FindFirstValue("external_access_token"),
                PictureUrl = identity.FindFirstValue("picture_url"),

            };
        }
    }
}