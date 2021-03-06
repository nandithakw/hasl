﻿using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Xcendant.HASL.API.Startup))]

namespace Xcendant.HASL.API
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

            WebApiConfig.Register(config);


            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            config.EnsureInitialized();

        }


    }
}
