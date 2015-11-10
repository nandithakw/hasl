using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Xcendant.Auth.Models.Entities;

namespace Xcendent.Auth.Providers
{
    public class LocalUserDetailsProvider : IUserDetailsProvider
    {
        public XcendentUser GetRegisteredUserInfo(ClaimsIdentity identity)
        {
            throw new NotImplementedException();
        }
    }
}