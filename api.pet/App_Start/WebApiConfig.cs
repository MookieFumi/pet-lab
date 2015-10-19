using System;
using System.Configuration;
using System.Web.Http;
using api.pet.Infrastructure.Attributes;

namespace api.pet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //var realm = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["realm"]);
            //config.Filters.Add(new VeemerBasicAuthentication(realm));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
