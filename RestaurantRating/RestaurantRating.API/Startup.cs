using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RestaurantRating.API.Startup))]
namespace RestaurantRating.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}