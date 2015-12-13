using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xcendant.Auth.Models.Entities;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Xcendent.Auth.Providers
{
    public class GoogleUserDetailsProvider : IUserDetailsProvider
    {
        public XcendentUser GetRegisteredUserInfo(ClaimsIdentity identity)
        {
            var url = @"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + identity.FindFirst("ExternalAccessToken");


            throw new NotImplementedException();
        }

        public Task<JObject> GetUserInfo(Dictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }
    }
}