using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Telecom.TourismControllers
{
    public class GlobalRegistrRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Telecom_TourismControllers_default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Telecom.TourismControllers.Controllers" }
            );

            routes.MapRoute(
                name: "Telecom_TourismControllers_error",
                url: "error/{action}/{error}/{dt}",
                defaults: new { controller = "ErrorPage", action = "Error500", error = UrlParameter.Optional,dt=UrlParameter.Optional },
                namespaces: new string[] { "Telecom.TourismControllers.Controllers" }
            );
        }
    }
}
