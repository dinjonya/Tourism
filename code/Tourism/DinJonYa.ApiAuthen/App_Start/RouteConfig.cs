﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DinJonYa.ApiAuthen
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DinJonYa.ApiAuthen.Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ApiAuthenHome", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
