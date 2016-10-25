using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using RestaurantRating.API.Factories;
using RestaurantRating.Domain;
using RestaurantRating.Repository.InMemory;
using RestaurantRating.Repository.Sql;

namespace RestaurantRating.API
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();

            //DI Container setup
            var container = new UnityContainer();
            container.LoadConfiguration();
            config.DependencyResolver = new UnityResolver(container);


            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(name: "DefaultRouting",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            //return json when browser request rest
            //config.Formatters.JsonFormatter.SupportedMediaTypes
            //    .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear(); //dont support xml

            //format Json
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("application/json-patch+json"));       // allow json-patch (partial update) operations 
            config.Formatters.JsonFormatter.SerializerSettings.Formatting
                = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();

            return config;
        }
    }
}
