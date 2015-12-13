using System;
using Autofac;
using Microsoft.AspNet.Identity.Owin;
using Xcendant.Auth.Models.DAL;
using Xcendant.Auth.Models.Entities;
using Xcendant.Auth.Models.Managers;
using Xcendant.Auth.Tests.Model.DAL;
using Xcendant.Auth.Tests.Model.Managers;
using NSubstitute;
using Microsoft.AspNet.Identity;
using Xcendant.Auth.Tests.Model.Entities;
using System.Threading.Tasks;

namespace Xcendant.Auth.Tests.Shared
{
    public class XcendantIdentityFixture : IDisposable
    {
        public IContainer Container { get; set; }
        public XcendantIdentityFixture()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<XcendantTestAuthContext>().As<AbstractXcendentAuthContext>().InstancePerLifetimeScope();

            var stubUserStore = new XcendantTestUserStore<XcendentUser>();

            //builder.Register(c => stubUserStore).
            //    As<AbstractXcendentUserStore<XcendentUser, AbstractXcendentAuthContext>>().InstancePerLifetimeScope();

            var stubIdentityOptions = new IdentityFactoryOptions<AbstractXcendentUserManager<XcendentUser>>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            };
            builder.Register(c => stubIdentityOptions);
            var stubUserManager = Substitute.For<XcendentTestUserManager>(stubUserStore, stubIdentityOptions);
            //stubUserManager.CreateAsync(new XcendentUser(), "").ReturnsForAnyArgs(Task.FromResult(IdentityResult.Success));
            //builder.RegisterType<XcendentTestUserManager>().As<AbstractXcendentUserManager<XcendentUser>>().InstancePerLifetimeScope();
            builder.Register(c => stubUserManager).As<AbstractXcendentUserManager<XcendentUser>>().InstancePerLifetimeScope();
            Container = builder.Build();
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
