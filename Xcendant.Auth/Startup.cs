using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Xcendant.Auth.Models.Managers;
using Xcendant.Auth.Models.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using Xcendant.Auth.Models.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(Xcendant.Auth.Startup))]

namespace Xcendant.Auth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var autoFacBuilder = new ContainerBuilder();
            HttpConfiguration config = new HttpConfiguration();

            ConfigureAutoFac(autoFacBuilder);
            autoFacBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            autoFacBuilder.RegisterWebApiFilterProvider(config);
            autoFacBuilder.RegisterWebApiModelBinders(Assembly.GetExecutingAssembly());
            var container = autoFacBuilder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            ConfigureAuth(app);
            WebApiConfig.Register(config);


            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            config.EnsureInitialized();

        }
    }
}
