using Autofac;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Members;
using Xcendant.HASL.Services.Members;

namespace Xcendant.HASL.API
{
    public partial class Startup
    {
        public void ConfigureAutoFac(ContainerBuilder autoFacBuilder)
        {
            autoFacBuilder.RegisterType<HaslContext>().As<IHaslContext>().InstancePerDependency();
            autoFacBuilder.RegisterType<RegisteredUserFacade>().As<IRegisteredUserFacade>().InstancePerRequest();
            autoFacBuilder.RegisterType<RegisteredUserManager>().As<IRegisteredUserManager>().InstancePerRequest();

        }
    }
}