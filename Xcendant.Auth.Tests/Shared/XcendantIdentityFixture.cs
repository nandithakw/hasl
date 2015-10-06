using System;
using Autofac;
using Microsoft.AspNet.Identity.Owin;
using Xcendant.Auth.Models.DAL;
using Xcendant.Auth.Models.Entities;
using Xcendant.Auth.Models.Managers;
using Xcendant.Auth.Tests.Model.DAL;
using Xcendant.Auth.Tests.Model.Managers;

namespace Xcendant.Auth.Tests.Shared
{
    public class XcendantIdentityFixture : IDisposable
    {
        public IContainer Container { get; set; }
        public XcendantIdentityFixture()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<XcendantTestAuthContext>().As<AbstractXcendentAuthContext>().InstancePerLifetimeScope();

            builder.Register(c => new XcendentUserStore<XcendentUser, AbstractXcendentAuthContext>(c.Resolve<AbstractXcendentAuthContext>())).
                As<AbstractXcendentUserStore<XcendentUser, AbstractXcendentAuthContext>>().InstancePerLifetimeScope();

            builder.Register(c => new IdentityFactoryOptions<AbstractXcendentUserManager<XcendentUser>>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });
            builder.RegisterType<XcendentTestUserManager>().As<AbstractXcendentUserManager<XcendentUser>>().InstancePerLifetimeScope();
            Container = builder.Build();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
