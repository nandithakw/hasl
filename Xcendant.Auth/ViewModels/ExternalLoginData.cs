using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Xcendent.Auth.ViewModels
{
    public class ExternalLoginData
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ExternalAccesToken { get; set; }

        public IList<Claim> GetClaims()
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

            if (Name != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, Name, null, LoginProvider));
            }
            if (Email != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, Email, null, LoginProvider));
            }

            if (ExternalAccesToken != null)
            {
                claims.Add(new Claim("ExternalAccessToken", ExternalAccesToken, null, ExternalAccesToken));
                
            }
            return claims;
        }

        public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
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

            return new ExternalLoginData
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                Name = identity.FindFirstValue(ClaimTypes.Name),
                Email = identity.FindFirstValue(ClaimTypes.Email),
                ExternalAccesToken = identity.FindFirstValue("ExternalAccessToken")
            };
        }
    }
}