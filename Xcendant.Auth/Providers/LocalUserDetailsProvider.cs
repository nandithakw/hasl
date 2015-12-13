using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using Xcendant.Auth.Models.Entities;

namespace Xcendent.Auth.Providers
{
    public class LocalUserDetailsProvider : IUserDetailsProvider
    {
        public XcendentUser GetRegisteredUserInfo(ClaimsIdentity identity)
        {
            throw new NotImplementedException();
        }

        public Task<JObject> GetUserInfo(Dictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }
    }
}