using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Telecom.TourismResources
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Telecom.TourismResources.Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ResourcesHome", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] { "Telecom.TourismResources.Controllers" }
            );
        }
    }
}
