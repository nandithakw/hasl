using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using Xcendent.Auth.Filteres;
using Xcendent.Auth.Models.Managers;

namespace Xcendant.Auth
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new SecurityExceptionFilterAttribute());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            var formatters = config.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            System.IdentityModel.Services.FederatedAuthentication.FederationConfigurationCreated += (s, e) =>
            {

                e.FederationConfiguration.IdentityConfiguration.ClaimsAuthorizationManager = new XcendantAuthorizationManager();
            };
        }
    }
}
