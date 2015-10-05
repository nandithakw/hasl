using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xcendent.Auth.Models.DAL;

namespace Xcendant.Auth.Tests.Model.DAL
{
    class XcendantTestAuthContext : AbstractXcendentAuthContext
    {

        public XcendantTestAuthContext(): base("DefaultConnection", false) 

        {
        }

        public static XcendantTestAuthContext Create()
        {
            return new XcendantTestAuthContext();
        }
    }
}
