using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Xcendant.Auth.Models.Entities;

namespace Xcendent.Auth.Providers
{
    public interface IUserDetailsProvider
    {
        XcendentUser GetRegisteredUserInfo(ClaimsIdentity identity);
        Task<JObject> GetUserInfo(Dictionary<string, string> properties);
    }
}