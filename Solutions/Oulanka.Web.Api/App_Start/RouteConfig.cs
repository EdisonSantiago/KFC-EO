using System.Web.Mvc;
using System.Web.Routing;
using Oulanka.Web.Core.Routing;

namespace Oulanka.Web.Api
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            OulankaRouteRegistrar.RegisterFromConfigRouteTable(routes);

            routes.MapRoute(
                "Default", 
                "{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

    }
}