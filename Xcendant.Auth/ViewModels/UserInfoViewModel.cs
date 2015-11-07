using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xcendant.Auth.ViewModels
{
    public class UserInfoViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}