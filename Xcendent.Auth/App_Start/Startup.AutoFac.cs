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
using Xcendent.Auth.Models.DAL;
using Xcendent.Auth.Models.Entities;
using Xcendent.Auth.Models.Managers;

namespace Xcendent.Auth
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