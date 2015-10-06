using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Xcendant.Auth.Models.DAL;
using Xcendant.Auth.Models.Entities;
using Xcendant.Auth.Models.Managers;

namespace Xcendant.Auth
{
    public partial class Startup
    {
        public void ConfigureAutoFac(ContainerBuilder autoFacBuilder)
        {
            autoFacBuilder.RegisterType<XcendentAuthContext>().As<AbstractXcendentAuthContext>().InstancePerRequest();

           
            autoFacBuilder.Register(c => new XcendentUserStore<XcendentUser, AbstractXcendentAuthContext>(c.Resolve<AbstractXcendentAuthContext>())).
				As<AbstractXcendentUserStore<XcendentUser, AbstractXcendentAuthContext>>().InstancePerRequest();

            autoFacBuilder.Register(c => new IdentityFactoryOptions<AbstractXcendentUserManager<XcendentUser>>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });
            autoFacBuilder.RegisterType<XcendentUserManager>().As<AbstractXcendentUserManager<XcendentUser>>().InstancePerRequest();

        }
    }
}