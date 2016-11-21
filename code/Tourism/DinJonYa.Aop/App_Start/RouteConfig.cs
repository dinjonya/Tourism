using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DinJonYa.Aop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DinJonYa.Aop_default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AopHome", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] { "DinJonYa.Aop.Controllers" }
            );
        }
    }
}
