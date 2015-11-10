using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xcendant.Auth.Models.Entities;
using System.Security.Claims;
namespace Xcendent.Auth.Providers
{
    public class GoogleUserDetailsProvider : IUserDetailsProvider
    {
        public XcendentUser GetRegisteredUserInfo(ClaimsIdentity identity)
        {
            var url = @"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + identity.FindFirst("ExternalAccessToken");


            throw new NotImplementedException();
        }
    }
}