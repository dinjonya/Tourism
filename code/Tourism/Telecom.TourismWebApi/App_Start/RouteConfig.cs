using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Telecom.TourismWebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Telecom_TourismWebApi_Route_Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "WebApiHome", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] { "Telecom.TourismWebApi.Controllers" }
            );
        }
    }
}
